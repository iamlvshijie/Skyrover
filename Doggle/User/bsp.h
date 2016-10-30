#include "stm8s.h"
#define NRF24L01_CE_PORT        GPIOC
#define NRF24L01_CE_PIN         GPIO_PIN_3

#define SPI_CS_PORT             GPIOA
#define SPI_CS_PIN              GPIO_PIN_3

#define CE(x) x>0? GPIO_WriteHigh(GPIOC, GPIO_PIN_3): GPIO_WriteLow(GPIOC, GPIO_PIN_3)

#define LED(x) x==0? GPIO_WriteHigh(GPIOD, GPIO_PIN_3): GPIO_WriteLow(GPIOD, GPIO_PIN_3)