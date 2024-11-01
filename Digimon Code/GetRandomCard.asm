//Used in Gear Savanna to give you the random cards from a pack

byte GetRandomCard(void)

{  
  RandomValue = ReturnRandom(100);
  RandomValue = RandomValue % 255;
  if (RandomValue == 0) 
    cardRarity = 0; //very rare
  
  else if (RandomValue < 5) 
    cardRarity = 1; //rare
  
  else if (RandomValue < 20) 
    cardRarity = 2; //uncommon
  
  else 
  {
    cardRarity = 4; //very common
    if (RandomValue < 50) 
     cardRarity = 3; //common
    
  }
  cardArray[] = AllocateArray(66); 
  cardQuantity = 0;
  currentcardOffset = 0;
  currentValue = 0;
  for (int i = 0; i < 66; i = i + 1) 
  {
      if (cardRarity == (&CardRarityOffset)[currentcardOffset]) 
      {
         cardArray[currentValue] = i;
         cardQuantity = (cardQuantity + 1) % 255;
         currentValue++;
      }
    currentcardOffset = currentcardOffset + 4;
  }
  RandomValue = ReturnRandom(cardQuantity);
  cardSelected = cardArray[RandomValue % 255];
  FreeArray(cardArray);
  return cardSelected;
}



Disassembly:
                             GetRandomCard
                           
        80106e0c e0 ff bd 27     addiu      sp,sp,-0x20
        80106e10 1c 00 bf af     sw         ra,local_4(sp)
        80106e14 18 00 b2 af     sw         s2,local_8(sp)
        80106e18 14 00 b1 af     sw         s1,local_c(sp)
        80106e1c 10 00 b0 af     sw         s0,local_10(sp)
        80106e20 b5 8d 02 0c     jal        0x800a36d4  //ReturnRandom         
        80106e24 64 00 04 24     _li        a0,0x64
        80106e28 ff 00 51 30     andi       s1,v0,0xff
        80106e2c 03 00 20 16     bne        s1,zero,0x80106e3c
        80106e30 00 00 00 00     _nop
        80106e34 10 00 00 10     b          0x80106e78
        80106e38 21 88 00 00     _clear     s1
                             LAB_80106e3c                                    
        80106e3c 05 00 21 2e     sltiu      at,s1,0x5
        80106e40 03 00 20 10     beq        at,zero,0x80106e50
        80106e44 00 00 00 00     _nop
        80106e48 0b 00 00 10     b          0x80106e78
        80106e4c 01 00 11 24     _li        s1,0x1
                             LAB_80106e50                                   
        80106e50 14 00 21 2e     sltiu      at,s1,0x14
        80106e54 03 00 20 10     beq        at,zero,0x80106e64
        80106e58 00 00 00 00     _nop
        80106e5c 06 00 00 10     b          0x80106e78
        80106e60 02 00 11 24     _li        s1,0x2
                             LAB_80106e64                                  
        80106e64 32 00 21 2e     sltiu      at,s1,0x32
        80106e68 03 00 20 10     beq        at,zero,0x80106e78
        80106e6c 04 00 11 24     _li        s1,0x4
        80106e70 01 00 00 10     b          0x80106e78
        80106e74 03 00 11 24     _li        s1,0x3
                             LAB_80106e78                                     
        80106e78 b4 f0 03 0c     jal        0x800fc2d0  //AllocateArray                                
        80106e7c 42 00 04 24     _li        a0,0x42
        80106e80 21 90 40 00     move       s2,v0
        80106e84 21 18 40 02     move       v1,s2
        80106e88 21 20 00 00     clear      a0
        80106e8c 21 80 00 00     clear      s0
        80106e90 10 00 00 10     b          0x80106ed4
        80106e94 21 28 00 00     _clear     a1
                             LAB_80106e98                                    
        80106e98 13 80 02 3c     lui        v0,0x8013
        80106e9c d9 ff 42 24     addiu      v0,v0,-0x27
        80106ea0 21 10 45 00     addu       v0,v0,a1
        80106ea4 00 00 42 90     lbu        v0,0x0(v0)      
        80106ea8 00 00 00 00     nop
        80106eac 06 00 22 16     bne        s1,v0,0x80106ec8
        80106eb0 00 00 00 00     _nop
        80106eb4 21 10 60 00     move       v0,v1
        80106eb8 01 00 43 24     addiu      v1,v0,0x1
        80106ebc 00 00 50 a0     sb         s0,0x0(v0)
        80106ec0 01 00 82 24     addiu      v0,a0,0x1
        80106ec4 ff 00 44 30     andi       a0,v0,0xff
                             LAB_80106ec8                                    
        80106ec8 01 00 02 26     addiu      v0,s0,0x1
        80106ecc ff 00 50 30     andi       s0,v0,0xff
        80106ed0 04 00 a5 20     addi       a1,a1,0x4
                             LAB_80106ed4                                   
        80106ed4 42 00 01 2e     sltiu      at,s0,0x42
        80106ed8 ef ff 20 14     bne        at,zero,0x80106e98
        80106edc 00 00 00 00     _nop
        80106ee0 b5 8d 02 0c     jal        0x800a36d4  //ReturnRandom                                
        80106ee4 00 00 00 00     _nop
        80106ee8 ff 00 51 30     andi       s1,v0,0xff
        80106eec 21 10 51 02     addu       v0,s2,s1
        80106ef0 00 00 50 90     lbu        s0,0x0(v0)
        80106ef4 c4 f0 03 0c     jal        0x800fc310  //FreeArray                   
        80106ef8 21 20 40 02     _move      a0,s2
        80106efc 21 10 00 02     move       v0,s0
        80106f00 1c 00 bf 8f     lw         ra,local_4(sp)
        80106f04 18 00 b2 8f     lw         s2,local_8(sp)
        80106f08 14 00 b1 8f     lw         s1,local_c(sp)
        80106f0c 10 00 b0 8f     lw         s0,local_10(sp)
        80106f10 08 00 e0 03     jr         ra
        80106f14 20 00 bd 27     _addiu     sp,sp,0x20

