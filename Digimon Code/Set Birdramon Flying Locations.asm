//Used to set the locations you can fly to with Birdramon

void SetBirdramonFlyingLocations(void)

{
  bool bVar1;
  byte bVar2;
  undefined3 extraout_var;
  byte *pbVar3;
  int iVar4;
  
  pbVar3 = *DAT_80134f68;
  *(DAT_80134f68 + 2) = 0;
  for (int i = 0; i < 6; i++) 
  {
    if (IsTriggerSet(BirdramonLocations[i].trigger) != 0) 
    {
      *(DAT_80134f68 + 2) = *(DAT_80134f68 + 2) + 1;
      if (CurrentMoney < BirdramonLocations[i].travelFee) 
      {
        *pbVar3 = i;
        pbVar3 = pbVar3 + 2;
      }
      else 
      {
        *pbVar3 = i | 0x80;
        pbVar3 = pbVar3 + 2;
      }
    }
  }
  return;
}


Disassembly:

                             
                             FUN_80107ab8                                   
        80107ab8 e0 ff bd 27     addiu      sp,sp,-0x20
        80107abc 1c 00 bf af     sw         ra,0x1c(sp)
        80107ac0 18 00 b2 af     sw         s2,0x18(sp)
        80107ac4 14 00 b1 af     sw         s1,0x14(sp)
        80107ac8 3c 94 82 8f     lw         v0,-0x6bc4(gp)  //DAT_80134f68
        80107acc 10 00 b0 af     sw         s0,0x10(sp)
        80107ad0 00 00 50 8c     lw         s0,0x0(v0)
        80107ad4 21 88 00 00     clear      s1
        80107ad8 08 00 40 a0     sb         zero,0x8(v0)
        80107adc 25 00 00 10     b          0x80107b74
        80107ae0 21 90 00 00     _clear     s2
                             LAB_80107ae4                                  
        80107ae4 13 80 02 3c     lui        v0,0x8013
        80107ae8 4c 02 42 24     addiu      v0,v0,0x24c
        80107aec 21 10 52 00     addu       v0,v0,s2
        80107af0 02 00 44 94     lhu        a0,0x2(v0)  //TransportTrigger                    
        80107af4 0f 19 04 0c     jal        IsTriggerSet                                   
        80107af8 00 00 00 00     _nop
        80107afc 1a 00 40 10     beq        v0,zero,0x80107b68
        80107b00 00 00 00 00     _nop
        80107b04 3c 94 83 8f     lw         v1,-0x6bc4(gp)  //DAT_80134f68
        80107b08 00 00 00 00     nop
        80107b0c 08 00 62 90     lbu        v0,0x8(v1)
        80107b10 00 00 00 00     nop
        80107b14 01 00 42 24     addiu      v0,v0,0x1
        80107b18 08 00 62 a0     sb         v0,0x8(v1)
        80107b1c 13 80 02 3c     lui        v0,0x8013
        80107b20 50 02 42 24     addiu      v0,v0,0x250
        80107b24 21 10 52 00     addu       v0,v0,s2
        80107b28 00 00 43 8c     lw         v1,0x0(v0)  //TravelFee                            
        80107b2c 8c 93 82 8f     lw         v0,-0x6c74(gp)  //CurrentMoney
        80107b30 00 00 00 00     nop
        80107b34 2b 08 43 00     sltu       at,v0,v1
        80107b38 07 00 20 14     bne        at,zero,0x80107b58
        80107b3c 00 00 00 00     _nop
        80107b40 21 10 00 02     move       v0,s0
        80107b44 80 00 23 36     ori        v1,s1,0x80
        80107b48 01 00 50 24     addiu      s0,v0,0x1
        80107b4c 00 00 43 a0     sb         v1,0x0(v0)
        80107b50 05 00 00 10     b          0x80107b68
        80107b54 01 00 10 26     _addiu     s0,s0,0x1
                             LAB_80107b58                                  
        80107b58 21 10 00 02     move       v0,s0
        80107b5c 01 00 50 24     addiu      s0,v0,0x1
        80107b60 00 00 51 a0     sb         s1,0x0(v0)
        80107b64 01 00 10 26     addiu      s0,s0,0x1
                             LAB_80107b68                                    
        80107b68 01 00 22 26     addiu      v0,s1,0x1
        80107b6c ff 00 51 30     andi       s1,v0,0xff
        80107b70 08 00 52 22     addi       s2,s2,0x8
                             LAB_80107b74                                  
        80107b74 06 00 21 2e     sltiu      at,s1,0x6
        80107b78 da ff 20 14     bne        at,zero,0x80107ae4
        80107b7c 00 00 00 00     _nop
        80107b80 1c 00 bf 8f     lw         ra,0x1c(sp)
        80107b84 18 00 b2 8f     lw         s2,0x18(sp)
        80107b88 14 00 b1 8f     lw         s1,0x14(sp)
        80107b8c 10 00 b0 8f     lw         s0,0x10(sp)
        80107b90 08 00 e0 03     jr         ra
        80107b94 20 00 bd 27     _addiu     sp,sp,0x20
