//All the code related to the RNG value displayed on one of the videos

int rand(void)
{
  int iVar1;
  
  iVar1 = newRand();
  return iVar1;
}
 
Disassembly:                           
                             int __stdcall rand
        8009127c 00 00 00 00     nop
        80091280 38 5a 02 08     j          0x800968e0    //newRand                                 
        80091284 00 00 00 00     _nop


void SetReadQueue(void)

{
  FUN_800e3674();
  SetObject(0x404,0,CallReadQueue,RenderRNG);
  return;
}
 
Disassembly:                                                         
                             SetReadQueue                                  
        800e36c8 e8 ff bd 27     addiu      sp,sp,-0x18
        800e36cc 10 00 bf af     sw         ra,0x10(sp)
        800e36d0 9d 8d 03 0c     jal        0x800e3674                                    
        800e36d4 00 00 00 00     _nop
        800e36d8 0e 80 06 3c     lui        a2,0x800e
        800e36dc 09 80 07 3c     lui        a3,0x8009
        800e36e0 04 04 04 24     li         a0,0x404
        800e36e4 21 28 00 00     clear      a1
        800e36e8 04 37 c6 24     addiu      a2,a2,0x3704  //CallReadQueue
        800e36ec d9 8b 02 0c     jal        0x800a2f64  //SetObject                                       
        800e36f0 d4 6a e7 24     _addiu     a3,a3,0x6908  //RenderRNG
        800e36f4 10 00 bf 8f     lw         ra,0x10(sp)
        800e36f8 00 00 00 00     nop
        800e36fc 08 00 e0 03     jr         ra
        800e3700 18 00 bd 27     _addiu     sp,sp,0x18


void RenderRNGText(int colour,int digits,int posX,short posY,int value,int offsetPtr)

{
  POLY_FT4 *poly;
  int digitCount;
  int byteDigits[];
  
  poly = (POLY_FT4 *)GSGetWorkBase();
  
  SetDigitsRNGArray(digits,(char*)value,&digitCount, &byteDigits);
  
  digitCount--;
  
  for (; -1 < digitCount; digitCount--) 
  {
    SetNumberDataPoly(poly, 256, 484);
	
    poly->r0 = TextColourArray[colour].Red;
    poly->g0 = TextColourArray[colour].Green;
    poly->b0 = TextColourArray[colour].Blue;
	
    SetUVDataPolyFT4(poly,(char)(byteDigits[digitCount] * 6 + 137), -100, 6, 10);
	
    SetPosDataPolyFT4(poly, (short)(posX + ((digits - 1) - digitCount) * 6), posY, 6, 10);
	
    AddPrim((void *)(*(int *)(DAT_80134ed0 + 4) + offsetPtr * 4),poly);
	
    poly = poly + 1; //sets the next POLY_FT4
  }
  GSSetWorkBase(poly);
  return;
}          

Disassembly:		   
                             FUN_80096658                                    
        80096658 a8 ff bd 27     addiu      sp,sp,-0x58
        8009665c 3c 00 bf af     sw         ra,0x3c(sp)
        80096660 38 00 be af     sw         s8,0x38(sp)
        80096664 34 00 b7 af     sw         s7,0x34(sp)
        80096668 30 00 b6 af     sw         s6,0x30(sp)
        8009666c 2c 00 b5 af     sw         s5,0x2c(sp)
        80096670 28 00 b4 af     sw         s4,0x28(sp)
        80096674 24 00 b3 af     sw         s3,0x24(sp)
        80096678 20 00 b2 af     sw         s2,0x20(sp)
        8009667c 1c 00 b1 af     sw         s1,0x14(sp)
        80096680 68 00 b1 8f     lw         s1,0x68(sp)
        80096684 6c 00 be 8f     lw         s8,0x6c(sp)
        80096688 18 00 b0 af     sw         s0,0x18(sp)
        8009668c 21 90 80 00     move       s2,a0
        80096690 21 a0 a0 00     move       s4,a1
        80096694 21 b0 c0 00     move       s6,a2
        80096698 0e 63 02 0c     jal        0x80098c38  //GSGetWorkBase                                   
        8009669c 21 b8 e0 00     _move      s7,a3
        800966a0 21 80 40 00     move       s0,v0
        800966a4 21 20 80 02     move       a0,s4
        800966a8 21 28 20 02     move       a1,s1
        800966ac 44 00 a6 27     addiu      a2,sp,0x44
        800966b0 fc 59 02 0c     jal        0x800967f0  //SetDigitsArrayRNG                                   
        800966b4 48 00 a7 27     _addiu     a3,sp,0x48
        800966b8 44 00 a2 8f     lw         v0,0x44(sp)
        800966bc ff ff 95 22     addi       s5,s4,-0x1
        800966c0 ff ff 51 20     addi       s1,v0,-0x1
        800966c4 40 10 12 00     sll        v0,s2,0x1
        800966c8 80 98 11 00     sll        s3,s1,0x2
        800966cc 20 90 52 00     add        s2,v0,s2
        800966d0 37 00 00 10     b          0x800967b0
        800966d4 80 a0 1e 00     _sll       s4,s8,0x2
                             LAB_800966d8                                    
        800966d8 00 01 05 24     li         a1,0x100
        800966dc ed 94 03 0c     jal        0x800e53b4  //SetNumberDataPoly                                 
        800966e0 e4 01 06 24     _li        a2,0x1e4
        800966e4 13 80 02 3c     lui        v0,0x8013
        800966e8 18 b9 42 24     addiu      v0,v0,-0x46e8
        800966ec 21 10 52 00     addu       v0,v0,s2
        800966f0 00 00 42 90     lbu        v0,0x0(v0)
        800966f4 21 20 00 02     move       a0,s0
        800966f8 04 00 02 a2     sb         v0,0x4(s0)
        800966fc 13 80 02 3c     lui        v0,0x8013
        80096700 19 b9 42 24     addiu      v0,v0,-0x46e7
        80096704 21 10 52 00     addu       v0,v0,s2
        80096708 00 00 42 90     lbu        v0,0x0(v0)
        8009670c 9c 00 06 24     li         a2,0x9c
        80096710 05 00 02 a2     sb         v0,0x5(s0)
        80096714 13 80 02 3c     lui        v0,0x8013
        80096718 1a b9 42 24     addiu      v0,v0,-0x46e6
        8009671c 21 10 52 00     addu       v0,v0,s2
        80096720 00 00 42 90     lbu        v0,0x0(v0)
        80096724 06 00 07 24     li         a3,0x6
        80096728 06 00 02 a2     sb         v0,0x6(s0)
        8009672c 0a 00 02 24     li         v0,0xa
        80096730 10 00 a2 af     sw         v0,0x10(sp)
        80096734 21 10 b3 03     addu       v0,sp,s3
        80096738 48 00 43 8c     lw         v1,0x48(v0)
        8009673c 00 00 00 00     nop
        80096740 40 10 03 00     sll        v0,v1,0x1
        80096744 20 10 43 00     add        v0,v0,v1
        80096748 40 10 02 00     sll        v0,v0,0x1
        8009674c 89 00 42 20     addi       v0,v0,0x89
        80096750 00 2c 02 00     sll        a1,v0,0x10
        80096754 ad 95 03 0c     jal        0x800e56b4  //SetUVDataPolyFT4                                
        80096758 03 2c 05 00     _sra       a1,a1,0x10
        8009675c 0a 00 02 24     li         v0,0xa
        80096760 10 00 a2 af     sw         v0,0x10(sp)
        80096764 22 18 b1 02     sub        v1,s5,s1
        80096768 40 10 03 00     sll        v0,v1,0x1
        8009676c 20 10 43 00     add        v0,v0,v1
        80096770 40 10 02 00     sll        v0,v0,0x1
        80096774 20 10 c2 02     add        v0,s6,v0
        80096778 00 2c 02 00     sll        a1,v0,0x10
        8009677c 03 2c 05 00     sra        a1,a1,0x10
        80096780 21 20 00 02     move       a0,s0
        80096784 21 30 e0 02     move       a2,s7
        80096788 bb 95 03 0c     jal        0x800e56ec  //SetPosDataPolyFT4                               
        8009678c 06 00 07 24     _li        a3,0x6
        80096790 a4 93 82 8f     lw         v0,-0x6c5c(gp)
        80096794 21 28 00 02     move       a1,s0
        80096798 04 00 42 8c     lw         v0,0x4(v0)
        8009679c 28 00 b0 24     addiu      s0,a1,0x28
        800967a0 b5 4a 02 0c     jal        0x80092ad4  //AddPrim                                          
        800967a4 21 20 54 00     _addu      a0,v0,s4
        800967a8 ff ff 31 22     addi       s1,s1,-0x1
        800967ac fc ff 73 22     addi       s3,s3,-0x4
                             LAB_800967b0                                    
        800967b0 c9 ff 21 06     bgez       s1,0x800966d8
        800967b4 21 20 00 02     _move      a0,s0
        800967b8 0a 63 02 0c     jal        0x80098c28  //GSSetWorkBase(poly)                                 
        800967bc 21 20 00 02     _move      a0,s0
        800967c0 3c 00 bf 8f     lw         ra,0x3c(sp)
        800967c4 38 00 be 8f     lw         s8,0x38(sp)
        800967c8 34 00 b7 8f     lw         s7,0x34(sp)
        800967cc 30 00 b6 8f     lw         s6,0x30(sp)
        800967d0 2c 00 b5 8f     lw         s5,0x2c(sp)
        800967d4 28 00 b4 8f     lw         s4,0x28(sp)
        800967d8 24 00 b3 8f     lw         s3,0x24(sp)
        800967dc 20 00 b2 8f     lw         s2,0x20(sp)
        800967e0 1c 00 b1 8f     lw         s1,0x1c(sp)
        800967e4 18 00 b0 8f     lw         s0,0x18(sp)
        800967e8 08 00 e0 03     jr         ra
        800967ec 58 00 bd 27     _addiu     sp,sp,0x58
		
		
		
void SetDigitsArrayRNG(int digits,char *value,int *count,int array)

{
  int iVar1;
  int iVar2;
  char allDigits [16];
  
  iVar1 = digits - 1;
  sprintf(allDigits, "%011d", value);
  
  for (iVar2 = 0; iVar2 < digits; iVar2 = iVar2 + 1) 
    array[iVar2] = allDigits[iVar2] - 48;
  
  *count = digits;
  for (iVar2 = iVar1; (-1 < iVar1 && array[iVar2] == 0); iVar2--) 
  {
    if (iVar1 != 0) 
      *count = *count - 1;
    
    iVar1--;
  }
  return;
}

Disassembly:                                                                                                  
                             SetDigitsArrayRNG                                    
        800967f0 c8 ff bd 27     addiu      sp,sp,-0x38
        800967f4 20 00 bf af     sw         ra,0x20(sp)
        800967f8 1c 00 b3 af     sw         s3,0x1c(sp)
        800967fc 18 00 b2 af     sw         s2,0x18(sp)
        80096800 14 00 b1 af     sw         s1,0x14(sp)
        80096804 21 88 80 00     move       s1,a0
        80096808 ff ff 23 22     addi       v1,s1,-0x1
        8009680c 10 00 b0 af     sw         s0,0x10(sp)
        80096810 21 98 60 00     move       s3,v1
        80096814 09 80 02 3c     lui        v0,0x8009
        80096818 21 40 a0 00     move       t0,a1
        8009681c 21 80 c0 00     move       s0,a2
        80096820 78 69 45 24     addiu      a1=,v0,0x6978   //string_%011d         
        80096824 21 90 e0 00     move       s2,a3
        80096828 28 00 a4 27     addiu      a0,sp,0x28
        8009682c a7 44 02 0c     jal        0x8009129c  //sprintf                                        
        80096830 21 30 00 01     _move      a2,t0
        80096834 80 10 11 00     sll        v0,s1,0x2
        80096838 21 10 42 02     addu       v0,s2,v0
        8009683c 21 20 00 00     clear      a0
        80096840 21 28 00 00     clear      a1
        80096844 21 38 20 02     move       a3,s1
        80096848 08 00 00 10     b          0x8009686c
        8009684c fc ff 46 24     _addiu     a2,v0,-0x4
                             LAB_80096850                                   
        80096850 21 10 a4 03     addu       v0,sp,a0
        80096854 28 00 42 80     lb         v0,0x28(v0)
        80096858 01 00 84 20     addi       a0,a0,0x1
        8009685c d0 ff 43 20     addi       v1,v0,-0x30
        80096860 23 10 c5 00     subu       v0,a2,a1
        80096864 00 00 43 ac     sw         v1,0x0(v0)
        80096868 04 00 a5 20     addi       a1,a1,0x4
                             LAB_8009686c                                    
        8009686c 2a 08 91 00     slt        at,a0,s1
        80096870 f7 ff 20 14     bne        at,zero,0x80096850
        80096874 00 00 00 00     _nop
        80096878 21 20 60 02     move       a0,s3
        8009687c 00 00 07 ae     sw         a3,0x0(s0)
        80096880 0e 00 00 10     b          0x800968bc
        80096884 80 18 04 00     _sll       v1,a0,0x2
                             LAB_80096888                                   
        80096888 21 10 43 02     addu       v0,s2,v1
        8009688c 00 00 42 8c     lw         v0,0x0(v0)
        80096890 00 00 00 00     nop
        80096894 0b 00 40 14     bne        v0,zero,0x800968c4
        80096898 00 00 00 00     _nop
        8009689c 05 00 80 10     beq        a0,zero,0x800968b4
        800968a0 00 00 00 00     _nop
        800968a4 00 00 02 8e     lw         v0,0x0(s0)
        800968a8 00 00 00 00     nop
        800968ac ff ff 42 20     addi       v0,v0,-0x1
        800968b0 00 00 02 ae     sw         v0,0x0(s0)
                             LAB_800968b4                                  
        800968b4 ff ff 84 20     addi       a0,a0,-0x1
        800968b8 fc ff 63 20     addi       v1,v1,-0x4
                             LAB_800968bc                                   
        800968bc f2 ff 81 04     bgez       a0,0x80096888
        800968c0 00 00 00 00     _nop
                             LAB_800968c4                                   
        800968c4 20 00 bf 8f     lw         ra,0x20(sp)
        800968c8 1c 00 b3 8f     lw         s3,0x1c(sp)
        800968cc 18 00 b2 8f     lw         s2,0x18(sp)
        800968d0 14 00 b1 8f     lw         s1,0x14(sp)
        800968d4 10 00 b0 8f     lw         s0,0x10(sp)
        800968d8 08 00 e0 03     jr         ra
        800968dc 38 00 bd 27     _addiu     sp,sp,0x38

	
	
void NewCallRandom(void)
{
  RNGCALL_COUNT = RNGCALL_COUNT + 1;
  (*(code *)&BIOS_CALL_0xA0)();  //This is a BIOS call, this one calls the function 2F which is the "generate and return a random number"
  return;
}

Disassembly:
                             NewCallRandom                                    
        800968e0 09 80 03 3c     lui        v1,0x8009
        800968e4 70 69 62 8c     lw         v0,0x6970(v1)  //RNGCALL_COUNT
        800968e8 a0 00 0a 24     li         t2,0xa0
        800968ec 01 00 42 24     addiu      v0,v0,0x1
        800968f0 70 69 62 ac     sw         v0,0x6970(v1)  //RNGCALL_COUNT
        800968f4 08 00 40 01     jr         t2  //BIOS_CALL_0xA0
        800968f8 2f 00 09 24     _li        t1,0x2f   //call the random function
        800968fc 00 00 00 00     nop
        80096900 d8 ff bd 27     addiu      sp,sp,-0x28
        80096904 20 00 bf af     sw         ra,0x20(sp)
		
		
		
void RenderRNG(void)
{
  RenderRNGText(7, 11, -120, -120, _RandValue, 5);
  RenderRNGText(2, 11, -120, -100, RNGCALL_COUNT, 5);
  return;
}

Disassembly:   
							  RenderRNG
		80096908 d8 ff bd 27     addiu      sp,sp,-0x28
        8009690c 20 00 bf af     sw         ra,0x20(sp)
        80096910 01 a0 02 3c     lui        v0,0xa001
        80096914 10 90 42 8c     lw         v0,-0x6ff0(v0)  //RandValue
        80096918 07 00 04 24     li         a0,0x7
        8009691c 10 00 a2 af     sw         v0,0x10(sp)
        80096920 05 00 02 24     li         v0,0x5
        80096924 14 00 a2 af     sw         v0,0x14(sp)
        80096928 88 ff 06 24     li         a2,-0x78
        8009692c 90 ff 07 24     li         a3,-0x70
        80096930 96 59 02 0c     jal        0x80096658  //RenderRNGText                               
        80096934 0b 00 05 24     _li        a1,0xb
        80096938 09 80 02 3c     lui        v0,0x8009
        8009693c 70 69 42 8c     lw         v0,0x6970(v0)
        80096940 02 00 04 24     li         a0,0x2
        80096944 10 00 a2 af     sw         v0,0x10(sp)
        80096948 05 00 02 24     li         v0,0x5
        8009694c 14 00 a2 af     sw         v0,0x14(sp)
        80096950 88 ff 06 24     li         a2,-0x78
        80096954 9c ff 07 24     li         a3,-0x64
        80096958 96 59 02 0c     jal        0x80096658  //RenderRNGText                               
        8009695c 0b 00 05 24     _li        a1,0xb
        80096960 20 00 bf 8f     lw         ra,0x20(sp)
        80096964 00 00 00 00     nop
        80096968 08 00 e0 03     jr         ra
        8009696c 28 00 bd 27     _addiu     sp,sp,0x28
		
		
Values for the new functions:
                             RNGCALL_COUNT                                
                                                                                          
        80096970 00 00 00 00     int        00000000h          //Yes, it will overflow at some point
        80096974 00              ??         00h
        80096975 00              ??         00h
        80096976 00              ??         00h
        80096977 00              ??         00h
                            string_%011d                                
        80096978 25 30 31        ds         "%011d"
                 31 64 00

