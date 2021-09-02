#include "API_KalmanFilter.h"

/** Function to start set any initial parameters and states for the filter */ 
void API_KalmanFilter_init()
{

}

/** Function hook to call filtering process as an API call */ 
void API_KalmanFilter_processData(HID_report_t* report)
{
    // DEBUG CODE -- Adds 50 to the x and y so you can see the filter call and 
    // display on the serial port. 
    report->filter.filteredX = report->ptp.x + 50;
    report->filter.filteredY = report->ptp.y + 50;

    // FILTER CODE GOES HERE // 
}