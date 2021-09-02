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
#define DEFAULT_COORDINATE_ARRAY_SIZE   15

// STRUCTS and TYPEDEFS
typedef struct
{
    int16_t filterX; 
    int16_t filterY;
    int16_t xPositions[DEFAULT_COORDINATE_ARRAY_SIZE]; // array of
    int16_t yPositions[DEFAULT_COORDINATE_ARRAY_SIZE]; // if set the firmware is contains unique code

} kalmanFilterInfo_t;

/******************* IMPORTANT FUNCTIONS ******************/

void API_KalmanFilter_init();

/****************** ACTIONS ***************************/

void API_KalmanFilter_processData(HID_report_t* report);

#ifdef __cplusplus
}
#endif

#endif //API_KALMAN_FILTER_H
