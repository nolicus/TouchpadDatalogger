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
    Serial.begin(115200);
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

    initialize_saved_reports(); //initialize state for determining touch events
    pinMode(13, OUTPUT);
    digitalWrite(13, LOW);
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
            calcPrintDataLoggerPacket(&report, fltrData);
        }
    }

    /* Handle incoming messages from user on serial */
    // if(Serial.available())
    // {
    //     char rxChar = Serial.read();
    //     switch(rxChar)
    //     {
    //     default:
    //         break;
    //     }
    // }

    // PRINT SAMPLE RATE // 
}

/******** Functions for Printing Data ***********/

void printPtpReport(HID_report_t * report)
{
  Serial.print(F("Report ID:\t0x"));
  Serial.println(report->reportID, HEX);
  Serial.print(F("Time:\t"));
  Serial.println(report->ptp.timeStamp);
  Serial.print(F("Contact ID:\t"));
  Serial.println(report->ptp.contactID);
  Serial.print(F("Confidence:\t")); //Microsoft says "Set when a contact is too large to be a finger" but that is backwards. It's clear if the object is too big.
  Serial.println(report->ptp.confidence); 
  Serial.print(F("Tip:\t"));// Set if the contact is on the surface of the digitizer, once this clears you know you have lift-off
  Serial.println(report->ptp.tip);
  Serial.print(F("X:\t"));
  Serial.println(report->ptp.x);
  Serial.print(F("Y:\t"));
  Serial.println(report->ptp.y);
  Serial.print(F("Buttons:\t"));
  Serial.println(report->ptp.buttons);
  Serial.print(F("Contact Count:\t"));
  Serial.println(report->ptp.contactCount); //Total number of contacts to be reported in a given report
  Serial.println();
   
}

/** Run's and prints any filtered filtered data based on the given PTP reports*/ 
void printFilterReport(HID_report_t *report)
{
    // API_KalmanFilter_processData(report); // Apply Kalmanfilter to Report

    Serial.print(F("Filtered X:\t"));
    Serial.println(report->filter.filteredX);
    Serial.print(F("Filtered Y:\t"));
    Serial.println(report->filter.filteredY);
    Serial.println(); 
}

/** Run's and prints any filtered filtered data based on the given PTP reports*/
void calcPrintDataLoggerPacket(HID_report_t *report, FilterReport_t fltrData)
{
    // API_KalmanFilter_processData(report); // Apply Kalmanfilter to Report

    uint8_t buffer[16];

    // clear packet
    for(uint8_t val : buffer)
    {
        val = 0; 
    }

    // Simple packet header
    buffer[0] = 0x6A; 
    buffer[1] = 0xD5; 

    // Simple packet footer
    buffer[14] = 0x17; 
    buffer[15] = 0xE2;

    // Payload
    buffer[2] = report->ptp.timeStamp & 0x00FF;
    buffer[3] = (report->ptp.timeStamp & 0xFF00) >> 8;

    buffer[4] = report->ptp.x & 0x00FF;
    buffer[5] = (report->ptp.x & 0xFF00) >> 8;

    buffer[6] = report->ptp.y & 0x00FF;
    buffer[7] = (report->ptp.y & 0xFF00) >> 8;

    buffer[8] = report->ptp.confidence;

    buffer[9] = fltrData.filteredX & 0x00FF;
    buffer[10] = (fltrData.filteredX & 0xFF00) >> 8;

    buffer[11] = fltrData.filteredY & 0x00FF;
    buffer[12] = (fltrData.filteredY & 0xFF00) >> 8;

    // Checksum
    uint8_t checksum = 0; 
    for(uint8_t i = 0; i < 13; i++)
    {
        checksum += buffer[i];
    }
    
    buffer[13] = checksum; 

    // Send data
    Serial.write(buffer, 16);

    // Flash the LED (DEBUG)
    digitalWrite(13, HIGH);
    delay(75);
    digitalWrite(13, LOW); 
}