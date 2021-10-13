// Copyright (c) 2021 Cirque Corp. Restrictions apply. See: www.cirque.com/sw-license

#include "API_C3.h"         /** < Provides API calls to interact with API_C3 firmware */
#include "API_Hardware.h"
#include "API_HostBus.h"    /** < Provides I2C connection to module */
#include "API_KalmanFilter.h"
#include "HID_Reports.h"

void printMouseReport(HID_report_t * report);

bool dataLoggerPrint_mode_g = true; /** <toggle for printing packets for datalogger> */ 

void setup()
{
    Serial.begin(2000000);
    delay(2000);

    Serial1.begin(115200);
    delay(2000);

    API_Hardware_init();       //Initialize board hardware
    API_Hardware_PowerOn();    //Power up the board
    delay(2);                  //delay for power up
    
    // initialize i2c connection at 400kHz 
    API_C3_init(PROJECT_I2C_FREQUENCY, ALPS_SLAVE_ADDR); 
    delay(50);                 //delay before reading registers after startup
    
    // Collect information about the system
    systemInfo_t sysInfo;
    API_C3_readSystemInfo(&sysInfo);
    API_C3_setPtpMode();

    pinMode(13, OUTPUT);
    digitalWrite(13, LOW);

    printPtpFilterHeader();
}

/** The main structure of the loop is: 
    Wait for the Data Ready line to assert. When it does, read the data (which clears DR) and analyze the data.
    The rest is just a user interface to change various settings.
    */
void loop()
{
    /* Handle incoming messages from module */
    if (API_C3_DR_Asserted()) // When Data is ready
    {
        HID_report_t report;
        API_C3_getReport(&report);             // read the report
        FilterReport_t fltrData = API_KalmanFilter_processData(&report); // filter the data

        /* Interpret report from module */
        if (dataLoggerPrint_mode_g)
        {
            printPtpFilterReport(report, fltrData);
        }
    }
}

/******** Functions for Printing Data ***********/

void printPtpFilterReport(HID_report_t &hidReport, FilterReport_t &fltrData)
{
    Serial.print(hidReport.reportID, HEX);
    Serial.print(",");
    Serial.print(hidReport.ptp.timeStamp);
    Serial.print(",");
    Serial.print(hidReport.ptp.contactID);
    Serial.print(",");
    Serial.print(hidReport.ptp.confidence);
    Serial.print(",");
    Serial.print(hidReport.ptp.tip);
    Serial.print(",");
    Serial.print(hidReport.ptp.x);
    Serial.print(",");
    Serial.print(hidReport.ptp.y);
    Serial.print(",");
    Serial.print(hidReport.ptp.buttons);
    Serial.print(",");
    Serial.print(hidReport.ptp.contactCount); //Total number of contacts to be reported in a given report
    Serial.print(",");
    Serial.print(fltrData.filteredX);
    Serial.print(",");
    Serial.print(fltrData.filteredY); //Total number of contacts to be reported in a given report
    Serial.println();
}

void printPtpFilterHeader()
{
    Serial.print("Report ID,");
    Serial.print("Time,");
    Serial.print("Contact ID,");
    Serial.print("Confidence,"); //Microsoft says "Set when a contact is too large to be a finger" but that is backwards. It's clear if the object is too big.
    Serial.print("Tip,");// Set if the contact is on the surface of the digitizer, once this clears you know you have lift-off
    Serial.print("X,");
    Serial.print("Y,");
    Serial.print("Buttons,");
    Serial.print("Contact Count,");
    Serial.print("Filtered X,");
    Serial.println("Filtered Y");
}