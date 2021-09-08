#include "API_KalmanFilter.h"
#include "HardwareSerial.h"

/** Function to start set any initial parameters and states for the filter */ 
void API_KalmanFilter_init()
{

}

/** Function hook to call filtering process as an API call */ 
FilterReport_t API_KalmanFilter_processData(HID_report_t* report)
{
    FilterReport_t fltr;
    // DEBUG CODE -- Adds 50 to the x and y so you can see the filter call and 
    // display on the serial port. 

    fltr.filteredX = report->ptp.x + 50;
    fltr.filteredY = report->ptp.y + 50;

    // FILTER CODE GOES HERE //
    return fltr;
}