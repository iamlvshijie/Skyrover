#include "stm8s_uart1.h"
#include "uart.h"
#include "stm8s_exti.h"
#include "nrf24l01.h"
#include "hal_spi.h"

u8 UART_RxBuffer[UART_RxBufferSize];
u8 UART_RX_NUM=0;
extern u8 nRF24L01_RxBuffer[];
extern u8 nRF24L01_Rx_Done;

EXTI_Sensitivity_TypeDef  Keyexti_Sensitivity_Type;

#pragma vector=1
__interrupt void TRAP_IRQHandler(void)
{
  
}
#pragma vector=2
__interrupt void TLI_IRQHandler(void)
{
  
}
#pragma vector=3
__interrupt void AWU_IRQHandler(void)
{
  
}
#pragma vector=4
__interrupt void CLK_IRQHandler(void)
{
  
  
}
#pragma vector=5
__interrupt void EXTI_PORTA_IRQHandler(void)
{
  
}
#pragma vector=6
__interrupt void EXTI_PORTB_IRQHandler(void)
{
  
}
#pragma vector=7
__interrupt void EXTI_PORTC_IRQHandler(void)
{
  
}
#pragma vector=8
__interrupt void EXTI_PORTD_IRQHandler(void)
{
  u8 status;
  Keyexti_Sensitivity_Type=EXTI_GetExtIntSensitivity(EXTI_PORT_GPIOD);
  if (Keyexti_Sensitivity_Type == EXTI_SENSITIVITY_FALL_ONLY)
  {
   
    CE(0);
    status=HAL_SPI_Read_Reg(STATUS);
    printf("INT0 status is 0x%x\r\n",status);
    
    if(status&RX_DR)
    {
      HAL_SPI_Read_Buffer(RD_RX_PLOAD,nRF24L01_RxBuffer,TX_PLOAD_WIDTH);// read receive payload from RX_FIFO buffer
      printf("rx a packet from channel %d\r\n", (status & RX_P_NO)>>1);
      HAL_SPI_Write_Reg(NRF24L01_WRITE_REG + STATUS,status);
      if( ((status & RX_P_NO)>>1) == nRF24L01_RxBuffer[1])
         nRF24L01_Rx_Done = 1; 
    }
    CE(1);
            
            
  }
}
#pragma vector=9
__interrupt void EXTI_PORTE_IRQHandler(void)
{
   
}
#ifdef STM8S903
#pragma vector=0xA
__interrupt void EXTI_PORTF_IRQHandler(void)
{
  
}
#endif
#ifdef STM8S208
#pragma vector=0xA
__interrupt void CAN_RX_IRQHandler(void)
{
  
}
#pragma vector=0xB
__interrupt void CAN_TX_IRQHandler(void)
{
  
}
#endif
#pragma vector=0xC
__interrupt void SPI_IRQHandler(void)
{
  
}
#pragma vector=0xD
__interrupt void TIM1_UPD_OVF_TRG_BRK_IRQHandler(void)
{
  
 // CleT1UPF;
 // PDODR->bit3=(~PDODR->bit3);
}
#pragma vector=0xE
__interrupt void TIM1_CAP_COM_IRQHandler(void)
{
  
}
#ifdef STM8S903
#pragma vector=0xF
__interrupt void TIM5_UPD_OVF_BRK_TRG_IRQHandler(void)
{
  
}
#pragma vector=0x10
__interrupt void TIM5_CAP_COM_IRQHandler(void)
{
  
}
#else
#pragma vector=0xF
__interrupt void TIM2_UPD_OVF_BRK_IRQHandler(void)
{
     
}
#pragma vector=0x10
__interrupt void TIM2_CAP_COM_IRQHandler(void)
{
  
}
#endif
#if defined (STM8S208) || defined(STM8S207) || defined(STM8S105)
#pragma vector=0x11
__interrupt void TIM3_UPD_OVF_BRK_IRQHandler(void)
{
  
}
#pragma vector=0x12
__interrupt void TIM3_CAP_COM_IRQHandler(void)
{
  
}
#endif

#if defined (STM8S208) || defined(STM8S207) || defined(STM8S105) || defined(STM8S103)
#pragma vector=0x13
__interrupt void UART1_TX_IRQHandler(void)
{
  
}
#pragma vector=0x14
__interrupt void UART1_RX_IRQHandler(void)
{ 
  u8 Res;
  if(UART1_GetITStatus(UART1_IT_RXNE )!= RESET)  
  {/*接收中断(接收到的数据必须是0x0d 0x0a结尾)*/
      Res =UART1_ReceiveData8();
      /*(USART1->DR);读取接收到的数据,当读完数据后自动取消RXNE的中断标志位*/
      if(( UART_RX_NUM&0x80)==0)/*接收未完成*/
      {
        if( UART_RX_NUM&0x40)/*接收到了0x0d*/
        {
          if(Res!=0x0a) 
            UART_RX_NUM=0;/*接收错误,重新开始*/
          else  
            UART_RX_NUM|=0x80;	/*接收完成了 */
        }
        else /*还没收到0X0D*/
        {	
          if(Res==0x0d) 
            UART_RX_NUM|=0x40;
          else
          {
            UART_RxBuffer[ UART_RX_NUM&0X3F]=Res ;
            UART_RX_NUM++;
            if( UART_RX_NUM>63) 
              UART_RX_NUM=0;/*接收数据错误,重新开始接收*/  
          }		 
        }
      }  		 
  }
}
#endif

#pragma vector=0x15
__interrupt void I2C_IRQHandler(void)
{
  
}

#ifdef STM8S105
#pragma vector=0x16
__interrupt void UART2_TX_IRQHandler(void)
{
   
}
#pragma vector=0x17
__interrupt void UART2_RX_IRQHandler(void)
{
   
}
#endif
#if defined(STM8S207) || defined(STM8S208)
#pragma vector=0x16
__interrupt void UART3_TX_IRQHandler(void)
{
  
}
#pragma vector=0x17
__interrupt void UART3_RX_IRQHandler(void)
{
  
}
#endif
#if defined(STM8S207) || defined(STM8S208)
#pragma vector=0x18
__interrupt void ADC2_IRQHandler(void)
{
   
}
#else
#pragma vector=0x18
__interrupt void ADC1_IRQHandler(void)
{
   
}
#endif
#ifdef STM8S903
#pragma vector=0x19
__interrupt void TIM6_UPD_OVF_TRG_IRQHandler(void)
{
  
}
#else
#pragma vector=0x19
__interrupt void TIM4_UPD_OVF_IRQHandler(void)
{
  
}
#endif
#pragma vector=0x1A
__interrupt void EEPROM_EEC_IRQHandler(void)
{
  
}


/******************* (C) COPYRIGHT 风驰iCreate嵌入式开发工作室 *****END OF FILE****/