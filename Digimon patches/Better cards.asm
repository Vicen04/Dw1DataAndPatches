byte GetRandomCard(void)

{  
  RandomValue = ReturnRandom(100);
  RandomValue = RandomValue % 255;
  if (RandomValue < 5) 
    cardRarity = 0; //very rare
  
  else if (RandomValue < 15) 
    cardRarity = 1; //rare
  
  else if (RandomValue < 35) 
    cardRarity = 2; //uncommon
  
  else 
  {
    cardRarity = 4; //very common
    if (RandomValue < 75) 
     cardRarity = 3; //common

//code ignored
    
  }

Disassembly:

                             GetRandomCard                        
        80106e0c e0 ff bd 27     addiu      sp,sp,-0x20
        80106e10 1c 00 bf af     sw         ra,local_4(sp)
        80106e14 18 00 b2 af     sw         s2,local_8(sp)
        80106e18 14 00 b1 af     sw         s1,local_c(sp)
        80106e1c 10 00 b0 af     sw         s0,local_10(sp)
        80106e20 b5 8d 02 0c     jal        0x800a36d4 //ReturnRandom          
        80106e24 64 00 04 24     _li        a0,0x64
        80106e28 05 00 41 2c     sltiu      at,v0,0x5
        80106e2c 03 00 20 10     beq        at,zero,0x80106e3c
        80106e30 00 00 00 00     _nop
        80106e34 10 00 00 10     b          0x80106e78
        80106e38 21 88 00 00     _clear     s1
                             LAB_80106e3c                                   
        80106e3c 0f 00 41 2c     sltiu      at,v0,0xf
        80106e40 03 00 20 10     beq        at,zero,0x80106e50
        80106e44 00 00 00 00     _nop
        80106e48 0b 00 00 10     b          0x80106e78
        80106e4c 01 00 11 24     _li        s1,0x1
                             LAB_80106e50                                  
        80106e50 23 00 41 2c     sltiu      at,v0,0x23
        80106e54 03 00 20 10     beq        at,zero,0x80106e64
        80106e58 00 00 00 00     _nop
        80106e5c 06 00 00 10     b          0x80106e78
        80106e60 02 00 11 24     _li        s1,0x2
                             LAB_80106e64                                 
        80106e64 4b 00 41 2c     sltiu      at,v0,0x4b
        80106e68 03 00 20 10     beq        at,zero,0x80106e78
        80106e6c 04 00 11 24     _li        s1,0x4
        80106e70 01 00 00 10     b          0x80106e78
        80106e74 03 00 11 24     _li        s1,0x3
                          