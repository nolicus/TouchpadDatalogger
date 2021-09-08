#ifndef API_KALMAN_FILTER_H
#define API_KALMAN_FILTER_H

#ifdef __cplusplus
extern "C"
{
#endif

// INCLUDES
#include <stdint.h>
#include <stdio.h>

#include "HID_Reports.h"

// POUND DEFINES and MACROS

/******************* IMPORTANT FUNCTIONS ******************/

void API_KalmanFilter_init();

/****************** ACTIONS ***************************/

FilterReport_t API_KalmanFilter_processData(HID_report_t* report);

#ifdef __cplusplus
}
#endif

#endif //API_KALMAN_FILTER_H
