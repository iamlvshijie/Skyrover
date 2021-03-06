#ifndef __NRF24L01_H
#define __NRF24L01_H
#include "stm32f0xx.h"

#define TX_ADR_WIDTH    5   
#define RX_ADR_WIDTH    5   
#define TX_PLOAD_WIDTH  32  
#define RX_PLOAD_WIDTH  32  

// SPI commands for nRF24L01
#define NRF24L01_READ_REG    	0x00 				// Define read command to register
#define NRF24L01_WRITE_REG   	0x20 				// Define write command to register
#define RD_RX_PLOAD 			0x61 				// Define RX payload register address
#define WR_TX_PLOAD 			0xA0 				// Define TX payload register address
#define FLUSH_TX    			0xE1 				// Define flush TX register command
#define FLUSH_RX    			0xE2 				// Define flush RX register command
#define REUSE_TX_PL 			0xE3 				// Define reuse TX payload register command
#define NOP         			0xFF 				// Define No Operation, might be used to read status register

// SPI registers' addresses for nRF24L01
#define CONFIG      			0x00				// 'Config' register address
#define EN_AA       			0x01                		// 'Enable Auto Acknowledgment' register address
#define EN_RXADDR   			0x02                		// 'Enabled RX addresses' register address
#define SETUP_AW    			0x03                		// 'Setup address width' register address
#define SETUP_RETR  			0x04                		// 'Setup Auto. Retrans' register address
#define RF_CH       			0x05                		// 'RF channel' register address
#define RF_SETUP    			0x06 				// 'RF setup' register address
#define STATUS      			0x07 				// 'Status' register address
	#define	RX_DR			0x40
	#define	TX_DS			0x20
	#define	MAX_RT		0x10
	#define RX_P_NO   0x0E
	#define TX_FULL   0x01
#define OBSERVE_TX  			0x08 				// 'Observe TX' register address
#define CD          			0x09 				//'Carrier Detect' register address
#define RX_ADDR_P0  			0x0A				// 'RX address pipe0' register address
#define RX_ADDR_P1  			0x0B 				// 'RX address pipe1' register address
#define RX_ADDR_P2  			0x0C 				// 'RX address pipe2' register address
#define RX_ADDR_P3  			0x0D 				// 'RX address pipe3' register address
#define RX_ADDR_P4  			0x0E 				// 'RX address pipe4' register address
#define RX_ADDR_P5  			0x0F				// 'RX address pipe5' register address
#define TX_ADDR     			0x10 				// 'TX address' register address
#define RX_PW_P0    			0x11 				// 'RX payload width, pipe0' register address
#define RX_PW_P1    			0x12 				// 'RX payload width, pipe1' register address
#define RX_PW_P2    			0x13 				// 'RX payload width, pipe2' register address
#define RX_PW_P3    			0x14 				// 'RX payload width, pipe3' register address
#define RX_PW_P4    			0x15 				// 'RX payload width, pipe4' register address
#define RX_PW_P5    			0x16 				// 'RX payload width, pipe5' register address
#define FIFO_STATUS 			0x17 			    	// 'FIFO Status Register' register address



typedef enum 
{
  HubMode = 0,
  TerminalMode = 1
} nRF24L01_ModeDef;

static void nRF24L01_DelayUS(uint8_t num);
uint32_t nRF24L01_Init(void);
uint32_t nRF24L01_Check(void);
uint32_t nRF24L01_Set_Mode(nRF24L01_ModeDef mode, uint8_t chn);
//void nRF24L01_TX_Mode(void);
//void nRF24L01_RX_Mode(void);
uint8_t 	nRF24L01_RxPacket(unsigned char *rx_buf);
void nRF24L01_TxPacket(unsigned char *tx_buf);
uint8_t nRF24L01_Read_Buf(uint8_t reg,uint8_t *pBuf,uint8_t len);
uint8_t nRF24L01_Write_Buf(uint8_t reg, uint8_t *pBuf, uint8_t len);

void nRF24L01_Config(void);
//void nRF24L01_Send(void);
//void nRF24L01_Receive(void);

#endif /*_NRF24L01_H*/

