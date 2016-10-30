
#include "nrf24l01.h"
#include <stdio.h>
#include <string.h>
#include "hal_spi.h"
#include "stm8s_exti.h"


//const u8 TX_ADDRESS[TX_ADR_WIDTH] = {0x31, 0x36, 0x32, 0x34, 0x30};
//const u8 RX_ADDRESS[RX_ADR_WIDTH] = {0x31, 0x36, 0x32, 0x34, 0x30};

const u8 RX_ADDRESS0[RX_ADR_WIDTH] = {0xFF, 0xFF, 0xFF, 0xFF, 0xFF};
const u8 RX_ADDRESS1[RX_ADR_WIDTH] = {0x31, 0x36, 0x32, 0x34, 0x30};
const u8 RX_ADDRESS2[1]= {0x32};									
const u8 RX_ADDRESS3[1]= {0x33};									
const u8 RX_ADDRESS4[1]= {0x34};									
const u8 RX_ADDRESS5[1]= {0x35};	



u8 RX_BUF[TX_PLOAD_WIDTH];
u8 TX_BUF[TX_PLOAD_WIDTH];

uint16_t BufferCmp(u8* pBuffer1, u8* pBuffer2, uint16_t BufferLength)
{
  while (BufferLength--)
  {
    if ((*pBuffer1) != *pBuffer2)
    {
      return BufferLength;
    }
    pBuffer1++;
    pBuffer2++;
  }

  return 0;
}


static void nRF24L01_DelayUS(u8 num)
{
	u8 i,j; 
	for(i=0;i>num;i++)
		for(j=100;j>0;j--);
}

uint32_t nRF24L01_Init(void)
{
  HAL_SPI_Init();
  
  /* CE CSN IRQ GPIO */
  GPIO_Init(GPIOC, GPIO_PIN_3, GPIO_MODE_OUT_PP_HIGH_FAST );
  GPIO_Init(GPIOA, GPIO_PIN_3, GPIO_MODE_OUT_PP_HIGH_FAST );
  GPIO_Init(GPIOD, GPIO_PIN_4, GPIO_MODE_IN_PU_IT);
  
  //EXTI_DeInit();
  //EXTI_SetTLISensitivity(EXTI_TLISENSITIVITY_FALL_ONLY);
  EXTI_SetExtIntSensitivity(EXTI_PORT_GPIOD, EXTI_SENSITIVITY_FALL_ONLY);  
  
  CE(0);
  
  HAL_SPI_CS_HIGH();
  
  //GPIO_WriteLow(GPIOC, GPIO_PIN_3);//CE=0, enable the chip
  
  if(nRF24L01_Check()==SUCCESS)
  {
    printf("nRF24L01 check successfully\r\n");
    return SUCCESS;
  }
  else
  {
    printf("nRF24L01 check failed\r\n");
    return ERROR;
  }
}

uint32_t nRF24L01_Check(void)
{
  u8 check_in_buf[5]={0x11,0x22,0x33,0x44,0x55};
  u8 check_out_buf[5]={0x00};

  HAL_SPI_Write_Buffer(NRF24L01_WRITE_REG+TX_ADDR, check_in_buf, 5);
  HAL_SPI_Read_Buffer(NRF24L01_READ_REG+TX_ADDR, check_out_buf, 5);
  
  if(BufferCmp(check_in_buf, check_out_buf, 5)==0)
    return SUCCESS;		 
  else
    return ERROR;
}	

 /* 
void nRF24L01_RX_Mode(void)
{
	CE(0);	  
	HAL_SPI_Write_Buffer(NRF24L01_WRITE_REG+TX_ADDR,(u8*)RX_ADDRESS0,RX_ADR_WIDTH);
	HAL_SPI_Write_Buffer(NRF24L01_WRITE_REG+RX_ADDR_P0,(u8*)RX_ADDRESS0,RX_ADR_WIDTH);
	HAL_SPI_Write_Buffer(NRF24L01_WRITE_REG+RX_ADDR_P1,(u8*)RX_ADDRESS1,RX_ADR_WIDTH);	
	HAL_SPI_Write_Buffer(NRF24L01_WRITE_REG+RX_ADDR_P2,(u8*)RX_ADDRESS2,1);	
	HAL_SPI_Write_Buffer(NRF24L01_WRITE_REG+RX_ADDR_P3,(u8*)RX_ADDRESS3,1);	
	HAL_SPI_Write_Buffer(NRF24L01_WRITE_REG+RX_ADDR_P4,(u8*)RX_ADDRESS4,1);	
	HAL_SPI_Write_Buffer(NRF24L01_WRITE_REG+RX_ADDR_P5,(u8*)RX_ADDRESS5,1);		
	
	HAL_SPI_Write_Reg(NRF24L01_WRITE_REG+EN_AA,0x3f);    	// all channels auto ack   
	HAL_SPI_Write_Reg(NRF24L01_WRITE_REG+EN_RXADDR,0x3f);	// all channels enabled
	HAL_SPI_Write_Reg(NRF24L01_WRITE_REG+RF_CH,0);	     	// 2.4G+		  
	HAL_SPI_Write_Reg(NRF24L01_WRITE_REG+RX_PW_P0,RX_PLOAD_WIDTH);// valid rx data width    
	HAL_SPI_Write_Reg(NRF24L01_WRITE_REG+RF_SETUP,0x0f);	// Tx 0db,2Mbps, low noise amp
	HAL_SPI_Write_Reg(NRF24L01_WRITE_REG+CONFIG, 0x0f);		// PWR_UP,EN_CRC,16BIT_CRC,
	CE(1); 
}						 
 
void nRF24L01_TX_Mode(void)
{														 
	CE(0);	    
	HAL_SPI_Write_Buffer(NRF24L01_WRITE_REG+TX_ADDR,(u8*)TX_ADDRESS,TX_ADR_WIDTH);//?TX???? 
	HAL_SPI_Write_Buffer(NRF24L01_WRITE_REG+RX_ADDR_P0,(u8*)RX_ADDRESS,RX_ADR_WIDTH); //??TX????,??????ACK	  
	HAL_SPI_Write_Reg(NRF24L01_WRITE_REG + RX_PW_P0, TX_PLOAD_WIDTH);
	
	HAL_SPI_Write_Reg(NRF24L01_WRITE_REG+EN_AA,0x01);     
	HAL_SPI_Write_Reg(NRF24L01_WRITE_REG+EN_RXADDR,0x01); 
	HAL_SPI_Write_Reg(NRF24L01_WRITE_REG+SETUP_RETR,0x1a);
	HAL_SPI_Write_Reg(NRF24L01_WRITE_REG+RF_CH,0);       
	HAL_SPI_Write_Reg(NRF24L01_WRITE_REG+RF_SETUP,0x0f);  
	HAL_SPI_Write_Reg(NRF24L01_WRITE_REG+CONFIG,0x0e);    
	CE(1);
}
*/

uint32_t nRF24L01_Set_Mode(nRF24L01_ModeDef mode, u8 pipe)	
{
  u8 temp_RX_ADDRESS[] = {0x30, 0x36, 0x32, 0x34, 0x30};
  if(mode == HubMode)
  {
    //printf("work as Hub\r\n");
    printf("Channel 0 Rx address is 0x%x 0x%x 0x%x 0x%x 0x%x\r\n", RX_ADDRESS0[0],RX_ADDRESS0[1],RX_ADDRESS0[2],RX_ADDRESS0[3],RX_ADDRESS0[4]);
    printf("Channel 1 Rx address is 0x%x 0x%x 0x%x 0x%x 0x%x\r\n", RX_ADDRESS1[0],RX_ADDRESS1[1],RX_ADDRESS1[2],RX_ADDRESS1[3],RX_ADDRESS1[4]);
    printf("Channel 2 Rx address is 0x%x\r\n", RX_ADDRESS2[0]);
    printf("Channel 3 Rx address is 0x%x\r\n", RX_ADDRESS3[0]);
    printf("Channel 4 Rx address is 0x%x\r\n", RX_ADDRESS4[0]);
    printf("Channel 5 Rx address is 0x%x\r\n", RX_ADDRESS5[0]);
    
    CE(0);
    HAL_SPI_Write_Buffer(NRF24L01_WRITE_REG+TX_ADDR,(u8*)RX_ADDRESS0,RX_ADR_WIDTH);
    HAL_SPI_Write_Buffer(NRF24L01_WRITE_REG+RX_ADDR_P0,(u8*)RX_ADDRESS0,RX_ADR_WIDTH);
    HAL_SPI_Write_Buffer(NRF24L01_WRITE_REG+RX_ADDR_P1,(u8*)RX_ADDRESS1,RX_ADR_WIDTH);	
    HAL_SPI_Write_Buffer(NRF24L01_WRITE_REG+RX_ADDR_P2,(u8*)RX_ADDRESS2,1);	
    HAL_SPI_Write_Buffer(NRF24L01_WRITE_REG+RX_ADDR_P3,(u8*)RX_ADDRESS3,1);	
    HAL_SPI_Write_Buffer(NRF24L01_WRITE_REG+RX_ADDR_P4,(u8*)RX_ADDRESS4,1);	
    HAL_SPI_Write_Buffer(NRF24L01_WRITE_REG+RX_ADDR_P5,(u8*)RX_ADDRESS5,1);		
    
    HAL_SPI_Write_Reg(NRF24L01_WRITE_REG+EN_AA,0x3f);    	// all channels auto ack   
    HAL_SPI_Write_Reg(NRF24L01_WRITE_REG+EN_RXADDR,0x3f);	// all channels enabled
    HAL_SPI_Write_Reg(NRF24L01_WRITE_REG+RF_CH,0);	     	// 2.4G+	

    HAL_SPI_Write_Reg(NRF24L01_WRITE_REG+RX_PW_P0,RX_PLOAD_WIDTH);// valid rx data width    
    HAL_SPI_Write_Reg(NRF24L01_WRITE_REG+RX_PW_P1,RX_PLOAD_WIDTH);// valid rx data width 
    HAL_SPI_Write_Reg(NRF24L01_WRITE_REG+RX_PW_P2,RX_PLOAD_WIDTH);// valid rx data width   
    HAL_SPI_Write_Reg(NRF24L01_WRITE_REG+RX_PW_P3,RX_PLOAD_WIDTH);// valid rx data width   
    HAL_SPI_Write_Reg(NRF24L01_WRITE_REG+RX_PW_P4,RX_PLOAD_WIDTH);// valid rx data width   
    HAL_SPI_Write_Reg(NRF24L01_WRITE_REG+RX_PW_P5,RX_PLOAD_WIDTH);// valid rx data width   
            
    HAL_SPI_Write_Reg(NRF24L01_WRITE_REG+RF_SETUP,0x0f);	// Tx 0db,2Mbps, low noise amp
    HAL_SPI_Write_Reg(NRF24L01_WRITE_REG+CONFIG, 0x0f);		// PWR_UP,EN_CRC,16BIT_CRC, RX
    CE(1);
            nRF24L01_DelayUS(10);
            return SUCCESS;
    }
    else if (mode == TerminalMode)
    {
      printf("work as terminal, pipe is %d\r\n", pipe);
      CE(0);
      if(pipe==0)
      {
              HAL_SPI_Write_Buffer(NRF24L01_WRITE_REG+TX_ADDR,(u8*)RX_ADDRESS0,RX_ADR_WIDTH);
              HAL_SPI_Write_Buffer(NRF24L01_WRITE_REG+RX_ADDR_P0,(u8*)RX_ADDRESS0,RX_ADR_WIDTH);
      }
      else if(pipe<6)
      {
              temp_RX_ADDRESS[0] = 0x30 + pipe;
              printf("Tx address is 0x%x 0x%x 0x%x 0x%x 0x%x\r\n", temp_RX_ADDRESS[0],temp_RX_ADDRESS[1],temp_RX_ADDRESS[2],temp_RX_ADDRESS[3],temp_RX_ADDRESS[4]);
              HAL_SPI_Write_Buffer(NRF24L01_WRITE_REG+TX_ADDR,(u8*)temp_RX_ADDRESS,RX_ADR_WIDTH);
              HAL_SPI_Write_Buffer(NRF24L01_WRITE_REG+RX_ADDR_P0,(u8*)temp_RX_ADDRESS,RX_ADR_WIDTH);
      }
      else 
              return ERROR;
      
      HAL_SPI_Write_Reg(NRF24L01_WRITE_REG+EN_AA,0x01);    	// 0 channel auto ack   
      HAL_SPI_Write_Reg(NRF24L01_WRITE_REG+EN_RXADDR,0x01);	// 0 channel enabled
      HAL_SPI_Write_Reg(NRF24L01_WRITE_REG+RF_CH,0);	     	// 2.4G  
      HAL_SPI_Write_Reg(NRF24L01_WRITE_REG+RX_PW_P0,RX_PLOAD_WIDTH);// valid rx data width 		
      HAL_SPI_Write_Reg(NRF24L01_WRITE_REG+RF_SETUP,0x0f);	// Tx 0db,2Mbps, low noise amp
      HAL_SPI_Write_Reg(NRF24L01_WRITE_REG+CONFIG, 0x0f);		// PWR_UP,EN_CRC,16BIT_CRC, RX
      CE(1);
      nRF24L01_DelayUS(10);
      return SUCCESS;
    }
    else
    {
      return ERROR;
    }
} 

void nRF24L01_Config(void)
{
  //initial io
	//CE(0);          //        CE=0 ;chip enable
	//CSN(1);       //CSN=1   Spi disable
	HAL_SPI_Write_Reg(NRF24L01_WRITE_REG + CONFIG, 0x0e); // Set PWR_UP bit, enable CRC(2 bytes) &Prim:RX. RX_DR enabled..
	HAL_SPI_Write_Reg(NRF24L01_WRITE_REG + EN_AA, 0x3f);
	HAL_SPI_Write_Reg(NRF24L01_WRITE_REG + EN_RXADDR, 0x3f); // Enable Pipe0
//	HAL_SPI_Write_Reg(NRF24L01_WRITE_REG + SETUP_AW, 0x02); // Setup address width=5 bytes
//	HAL_SPI_Write_Reg(NRF24L01_WRITE_REG + SETUP_RETR, 0x1a); // 500us + 86us, 10 retrans...
	HAL_SPI_Write_Reg(NRF24L01_WRITE_REG + RF_CH, 0);
	HAL_SPI_Write_Reg(NRF24L01_WRITE_REG + RF_SETUP,0x07); // TX_PWR:0dBm, Datarate:2Mbps,
}

u8 nRF24L01_RxPacket(u8* rx_buf)
{

  u8 status,revale=0;
	CE(0);
	//printf("enter rx packet\r\n");
//	nRF24L01_DelayUS(10);
//	status=HAL_SPI_Receive_Byte(SPI1,STATUS);	

//	if(status & RX_DR)	
//	{	
		//printf("Rx a packet: status is 0x%x\r\n",status);
		HAL_SPI_Read_Buffer(RD_RX_PLOAD,rx_buf,TX_PLOAD_WIDTH);// read receive payload from RX_FIFO buffer
		revale =1;			//读取数据完成标志
//	}
	HAL_SPI_Write_Reg(NRF24L01_WRITE_REG + STATUS,status);   //接收到数据后RX_DR,TX_DS,MAX_PT都置高为1，通过写1来清楚中断标志
	CE(1);
	return revale;	
}


void nRF24L01_TxPacket(u8* tx_buf)
{
  u8 status = 0x00;
  CE(0);
  HAL_SPI_Write_Buffer(WR_TX_PLOAD, tx_buf, TX_PLOAD_WIDTH); 			
  HAL_SPI_Write_Reg(NRF24L01_WRITE_REG + CONFIG, 0x0e);   		
  nRF24L01_DelayUS(15);
  CE(1);		
  
  while(GPIO_ReadInputPin(GPIOD, GPIO_PIN_4)==1);
  status=HAL_SPI_Read_Reg(STATUS);
  HAL_SPI_Write_Reg(NRF24L01_WRITE_REG + STATUS, status);
  if(status&TX_DS)	
  {
          printf("TX_DS\r\n");
  }
  if(status&MAX_RT)
  {
          HAL_SPI_Write_Reg(FLUSH_TX, 0xFF);
          printf("MAX_RT\r\n");
  }
  
  CE(0);			
  HAL_SPI_Write_Reg(NRF24L01_WRITE_REG + CONFIG, 0x0f); 
  nRF24L01_DelayUS(15);
  CE(1);	
}

	


//void nRF24L01_Send(void)
//{
//    u8 status=0x00;
////	CSN(0);
//	//TX_Mode(TX_BUF);
//	while(IRQ);
//
//	CE(0);
//	//nRF24L01_DelayUS(10);
//	status=HAL_SPI_Read_Reg(STATUS);	// 读取状态寄存其来判断数据接收状况
//	if(status&TX_DS)	/*tx_ds == 0x20*/
//	{
//		printf("STATUS接受状态：0x%2x\r\n",status);
//		printf("\r\n发送完数据：%s\r\n",RX_BUF);	
//		HAL_SPI_Write_Reg(NRF24L01_WRITE_REG + STATUS, 0x20);      // 清除TX，让IRQ拉低；
//			
//	}
//	else if(status&MAX_RT)
//		{
//			printf("发送达到最大发送次数");	
//			HAL_SPI_Write_Reg(NRF24L01_WRITE_REG + STATUS, 0x10);      // 清除TX，让IRQ拉低；			
//		}
//	CE(1);
////	status=20;
//}


/*
void nRF24L01_Receive(void)
{   
  u8 i,status=0x01;  

	while(IRQ);
	printf("Rx a Packet\n");
	CE(0);
	nRF24L01_DelayUS(10);
	//status=SPI_Read_Reg(STATUS);	// 读取状态寄存其来判断数据接收状况
	printf("STATUS接受状态：0x%2x\r\n",status);
	if(status & 0x40)			//接受中断标志位
	{
		printf("接受成功");
//		CE(1);
		HAL_SPI_Read_Buffer(RD_RX_PLOAD,RX_BUF,TX_PLOAD_WIDTH);// read receive payload from RX_FIFO buffer

		printf("\r\n i=%d,接收到数据：%x\r\n",i,RX_BUF[0]);
//		for(i=0;i<32;i++)
//		{
//			RX_BUF[1] = 0X06;
//			printf("\r\n i=%d,接收到数据：%x\r\n",i,RX_BUF[i]);
//		}
		HAL_SPI_Write_Reg(NRF24L01_WRITE_REG + STATUS, 0x40);      // 清除TX，让IRQ拉低
	}  
	CE(1);

}
*/









