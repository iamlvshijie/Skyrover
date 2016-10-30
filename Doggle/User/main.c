
/******************************* (C) COPYRIGHT  I2T ******************************
* File: main.c
* Description: 
* Author: 
* History:
**********************************************************************************/

/* Includes ------------------------------------------------------------------*/
/* Includes ------------------------------------------------------------------*/
#include "stm8s.h"
#include "stm8s_clk.h"
#include "intrinsics.h"
#include "stm8s_uart1.h"
#include "uart.h"
#include "nrf24l01.h"
#include "bsp.h"

void Delay(u32 nCount);
extern u8 UART_RxBuffer[UART_RxBufferSize];


extern u8 UART_RX_NUM;

/*
byte:   0       1       2               3
      len       channel Reserved        data
*/
u8 UART_TxBuffer[RX_PLOAD_WIDTH];

/*
byte:   0       1       2               3
      len       channel Reserved        data
*/
u8 nRF24L01_RxBuffer[RX_PLOAD_WIDTH];
volatile u8 nRF24L01_Rx_Done = 0;
volatile u8 nRF24L01_Rx_Channel = 0;

/* Private defines -----------------------------------------------------------*/
/* Private function prototypes -----------------------------------------------*/
/* Private functions ---------------------------------------------------------*/

int main(void)
{
   u8 len, index, check_sum;
  /* Infinite loop */
 
  CLK_HSIPrescalerConfig(CLK_PRESCALER_HSIDIV1);//internal 16MHz Core Clock
  /*!<Set High speed internal clock  */

  Uart_Init(); 
  
  //led
  GPIO_Init(GPIOD, GPIO_PIN_3, GPIO_MODE_OUT_PP_HIGH_FAST );
  
  if(nRF24L01_Init()==SUCCESS)
    GPIO_WriteLow(GPIOD, GPIO_PIN_3);
   // LED(1);

  __enable_interrupt(); 
  
  printf("@Project:PSS Doggle by I2T Team\r\n");
  printf("@Email:lvshijie@pku.edu.cn\r\n");
  printf("@Copyright:Skyrover Tech Inc.\r\n");
 
  nRF24L01_Set_Mode(HubMode, NULL);
  
  while (1)
  {
    if(UART_RX_NUM&0x80)
    {
      len=UART_RX_NUM&0x3f;
      //cmd parsing
      //UART1_SendString("You sent the messages is:",sizeof("You sent the messages is"));
      //UART1_SendString(UART_RxBuffer,len);
      //UART1_SendByte('\n');      
      
      UART_RX_NUM = 0;
    }
    
    if(nRF24L01_Rx_Done==1)
    {
      __disable_interrupt();
      
      len = nRF24L01_RxBuffer[0];
      check_sum = 0x00;
      
      printf("len is %d, nRF24L01_RxBuffer is ", len);
      
      for(index = 0; index<len; index++)
      { 
        printf("0x%x ",nRF24L01_RxBuffer[index+1]);
        check_sum = nRF24L01_RxBuffer[index+1]^check_sum;
      }
      printf("check sum is %d\r\n",check_sum);
      
      //check sum is right
      if(check_sum == nRF24L01_RxBuffer[len+1])
      {       
        for(index = 0; index < len+1; index++)
        {         
          UART1_SendByte(nRF24L01_RxBuffer[index]);
        }
        printf("\r\n");
      }
      else
      {
       printf("discard this packet\r\n");//discard this packet
      }
      nRF24L01_Rx_Done = 0;
      __enable_interrupt();
    }
  }
}

void Delay(u32 nCount)
{
  /* Decrement nCount value */
  while (nCount != 0)
  {
    nCount--;
  }
}



#ifdef USE_FULL_ASSERT

/**
  * @brief  Reports the name of the source file and the source line number
  *   where the assert_param error has occurred.
  * @param file: pointer to the source file name
  * @param line: assert_param error line source number
  * @retval : None
  */
void assert_failed(u8* file, u32 line)
{ 
  /* User can add his own implementation to report the file name and line number,
     ex: printf("Wrong parameters value: file %s on line %d\r\n", file, line) */

  /* Infinite loop */
  while (1)
  {
  }
}
#endif

/******************* (C) COPYRIGHT I2T *****END OF FILE****/
