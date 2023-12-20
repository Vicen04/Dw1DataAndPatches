int LoadMetalmamemonText(int currentTextLocation,uint currentTextValue)  // currentTextLocation is used to determine which text should be loaded, currentTextValue is used to determine the line where it should be loaded

{
  char cVar1;
  uint uVar2;
  vector<int> textAddresses;
  vector<byte> textLocations;
  ptr pointerText;
  ptr pointerLoaction;
  int iVar3;
  
  pointerText = &ptr_metalmamemonText; // It is located at 8005b318 in the code
  textAdresses = new vector<int>;
  for (int i = 0; i < 61; i++)
  {
   textAdresses.push_back(*pointerText); // value in that address
   pointerLocation = pointerText + 4;   // gets the next pointer
  }

  pointerLocation = &ptr_metalmamemonLocations; // It is located at 8005b40c in the code
  
  textLocations = new vector<byte>;
  
  for (int i = 0; i < 10; i++)
  {
   textLocations.push_back(*pointerLocation); // value in that address
   pointerLocation = pointerLocation + 1;   // gets the next pointer
  }
 
  uVar2 = currentTextValue % 3;
  if (((int)currentTextValue < 0) && (currentTextValue != 0)) {
    uVar2 = uVar2 - 4;
  }
  if (uVar2 == 0) {
    SetTextColor(7); // SetTextColor(byte color) from sydMontague github
  }
  else {
    SetTextColor(1);
  }
  cVar1 = textLocations(currentTextLocation);
  RenderString(textAdresses[(int)cVar1 + currentTextValue],0,currentTextValue * 13 + 1); // RenderString(stringPtr, xPos, yPos) from sydMontague github
  iVar3 = (int)textLocations(currentTextLocation + 1) - (int)cVar1;
  if (iVar3 < 0) {
    iVar3 = iVar3 + 3;
  }
  return (int)(char)(iVar3 >> 2);
}                             

        Offset       Hex         command

        80059fa8 e0 fe bd 27     addiu      sp,sp,-0x120
        80059fac 1c 00 bf af     sw         ra,0x1c(sp)
        80059fb0 18 00 b2 af     sw         s2,0x18(sp)
        80059fb4 14 00 b1 af     sw         s1,0x14(sp)
        80059fb8 06 80 18 3c     lui        t8,0x8006
        80059fbc 10 00 b0 af     sw         s0,0x10(sp)
        80059fc0 21 88 80 00     move       s1,a0
        80059fc4 18 b3 18 27     addiu      t8,t8,-0x4ce8
        80059fc8 20 00 af 27     addiu      t7,sp,0x20
        80059fcc 3d 00 19 24     li         t9,0x3d
                             LAB_80059fd0                                   
        80059fd0 00 00 0e 8f     lw         t6,0x0(t8) // ptr_metalmamemonText                                                                                                     
        80059fd4 ff ff 39 27     addiu      t9,t9,-0x1
        80059fd8 00 00 ee ad     sw         t6,0x0(t7)                                                                                              
        80059fdc 04 00 18 27     addiu      t8,t8,0x4
        80059fe0 fb ff 20 1f     bgtz       t9,LAB_80059fd0
        80059fe4 04 00 ef 25     _addiu     t7,t7,0x4
        80059fe8 06 80 18 3c     lui        t8,0x8006
        80059fec 0c b4 18 27     addiu      t8,t8,-0x4bf4
        80059ff0 14 01 af 27     addiu      t7,sp,0x114
        80059ff4 0a 00 19 24     li         t9,0xa
                             LAB_80059ff8                                    
        80059ff8 00 00 0e 83     lb         t6,0x0(t8) //ptr_metalmamemonLocations                        
        80059ffc ff ff 39 27     addiu      t9,t9,-0x1
        8005a000 00 00 ee a1     sb         t6,0x0(t7)
        8005a004 01 00 18 27     addiu      t8,t8,0x1
        8005a008 fb ff 20 1f     bgtz       t9,LAB_80059ff8
        8005a00c 01 00 ef 25     _addiu     t7,t7,0x1
        8005a010 21 80 a0 00     move       s0,a1
        8005a014 04 00 a1 04     bgez       a2,LAB_8005a028
        8005a018 03 00 a2 30     _andi      v0,a1,0x3
        8005a01c 02 00 40 10     beq        v0,zero,LAB_8005a028
        8005a020 00 00 00 00     _nop
        8005a024 fc ff 42 24     addiu      v0,v0,-0x4
                             LAB_8005a028                                   
        8005a028 05 00 40 14     bne        v0,zero,LAB_8005a040
        8005a02c 00 00 00 00     _nop
        8005a030 03 33 04 0c     jal        0x8010cc0c  // SetTextColor(a0)                                     
        8005a034 07 00 04 24     _li        a0,0x7
        8005a038 04 00 00 10     b          LAB_8005a04c
        8005a03c 21 10 b1 03     _addu      v0,sp,s1
                             LAB_8005a040                                   
        8005a040 03 33 04 0c     jal        0x8010cc0c   // SetTextColor(a0)                                    
        8005a044 01 00 04 24     _li        a0,0x1
        8005a048 21 10 b1 03     addu       v0,sp,s1
                             LAB_8005a04c                                    
        8005a04c 14 01 42 80     lb         v0,0x114(v0)
        8005a050 21 28 00 00     clear      a1
        8005a054 21 90 40 00     move       s2,v0
        8005a058 20 10 50 00     add        v0,v0,s0
        8005a05c 80 10 02 00     sll        v0,v0,0x2
        8005a060 21 18 a2 03     addu       v1,sp,v0
        8005a064 40 10 10 00     sll        v0,s0,0x1
        8005a068 20 10 50 00     add        v0,v0,s0
        8005a06c 80 10 02 00     sll        v0,v0,0x2
        8005a070 20 10 50 00     add        v0,v0,s0
        8005a074 20 00 64 8c     lw         a0,0x20(v1)
        8005a078 c9 33 04 0c     jal        0x8010cf24  // RenderString(a0, a1, a2)                                  
        8005a07c 01 00 46 20     _addi      a2,v0,0x1
        8005a080 01 00 22 22     addi       v0,s1,0x1
        8005a084 21 10 a2 03     addu       v0,sp,v0
        8005a088 14 01 42 80     lb         v0,0x114(v0)
        8005a08c 00 00 00 00     nop
        8005a090 22 10 52 00     sub        v0,v0,s2
        8005a094 03 00 41 04     bgez       v0,0x8005a0a4 // LAB_8005a0a4
        8005a098 83 c8 02 00     _sra       t9,v0,0x2
        8005a09c 03 00 42 24     addiu      v0,v0,0x3
        8005a0a0 83 c8 02 00     sra        t9,v0,0x2
                             LAB_8005a0a4                                   
        8005a0a4 00 16 19 00     sll        v0,t9,0x18
        8005a0a8 1c 00 bf 8f     lw         ra,0x1c(sp)
        8005a0ac 18 00 b2 8f     lw         s2,0x18(sp)
        8005a0b0 14 00 b1 8f     lw         s1,0x14(sp)
        8005a0b4 10 00 b0 8f     lw         s0,0x10(sp)
        8005a0b8 03 16 02 00     sra        v0,v0,0x18
        8005a0bc 08 00 e0 03     jr         ra
        8005a0c0 20 01 bd 27     _addiu     sp,sp,0x120
