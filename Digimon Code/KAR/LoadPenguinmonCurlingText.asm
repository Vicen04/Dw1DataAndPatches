int LoadPenguinmonText(int currentTextLocation,uint currentTextValue) // currentTextLocation is used to determine which text should be loaded, currentTextValue is used to determine the line where it should be loaded
{
  char cVar1;
  uint uVar2;
  vector<int> textAddresses;
  vector<byte> textLocations;
  ptr pointerText;
  ptr pointerLoaction;
  int iVar7;
  
  pointerText = &ptr_penguinmonText; // It is located at 8005af58 in the code
  textAdresses = new vector<int>;
  for (int i = 0; i < 61; i++)
  {
   textAdresses.push_back(*pointerText); // value in that address
   pointerLocation = pointerText + 4;   // gets the next pointer
  }

  pointerLocation = &ptr_penguinmonLocations; // It is located at 8005b04c in the code
  
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
  iVar7 = (int)textLocations(currentTextLocation + 1) - (int)cVar1;
  if (iVar7 < 0) {
    iVar7 = iVar7 + 3;
  }
  return (int)(char)(iVar7 >> 2);
}  

        Offset       Hex        Commands

        80059e8c e0 fe bd 27     addiu      sp,sp,-0x120
        80059e90 1c 00 bf af     sw         ra,0x1c(sp)
        80059e94 18 00 b2 af     sw         s2,0x18(sp)
        80059e98 14 00 b1 af     sw         s1,0x14(sp)
        80059e9c 06 80 18 3c     lui        t8,0x8006
        80059ea0 10 00 b0 af     sw         s0,0x10(sp)
        80059ea4 21 88 80 00     move       s1,a0
        80059ea8 58 af 18 27     addiu      t8,t8,-0x50a8
        80059eac 20 00 af 27     addiu      t7,sp,0x20
        80059eb0 3d 00 19 24     li         t9,0x3d
                             LAB_80059eb4                                         
        80059eb4 00 00 0e 8f     lw         t6,0x0(t8) // ptr_penguinmonText                                                                                              
        80059eb8 ff ff 39 27     addiu      t9,t9,-0x1
        80059ebc 00 00 ee ad     sw         t6,0x0(t7)                                                                                            
        80059ec0 04 00 18 27     addiu      t8,t8,0x4
        80059ec4 fb ff 20 1f     bgtz       t9,LAB_80059eb4
        80059ec8 04 00 ef 25     _addiu     t7,t7,0x4
        80059ecc 06 80 18 3c     lui        t8,0x8006
        80059ed0 4c b0 18 27     addiu      t8,t8,-0x4fb4
        80059ed4 14 01 af 27     addiu      t7,sp,0x114
        80059ed8 0a 00 19 24     li         t9,0xa
                             LAB_80059edc                                    
        80059edc 00 00 0e 83     lb         t6,0x0(t8) // ptr_penguinmonLocations                
        80059ee0 ff ff 39 27     addiu      t9,t9,-0x1
        80059ee4 00 00 ee a1     sb         t6,0x0(t7)
        80059ee8 01 00 18 27     addiu      t8,t8,0x1
        80059eec fb ff 20 1f     bgtz       t9,LAB_80059edc
        80059ef0 01 00 ef 25     _addiu     t7,t7,0x1
        80059ef4 21 80 a0 00     move       s0,a1
        80059ef8 04 00 a1 04     bgez       a1,LAB_80059f0c
        80059efc 03 00 a2 30     _andi      v0,a1,0x3
        80059f00 02 00 40 10     beq        v0,zero,LAB_80059f0c
        80059f04 00 00 00 00     _nop
        80059f08 fc ff 42 24     addiu      v0,v0,-0x4
                             LAB_80059f0c                                    
        80059f0c 05 00 40 14     bne        v0,zero,LAB_80059f24
        80059f10 00 00 00 00     _nop
        80059f14 03 33 04 0c     jal        0x8010cc0c   // SetTextColor(a0)                                     
        80059f18 07 00 04 24     _li        a0,0x7
        80059f1c 04 00 00 10     b          LAB_80059f30
        80059f20 21 10 b1 03     _addu      v0,sp,s1
                             LAB_80059f24                                    
        80059f24 03 33 04 0c     jal        0x8010cc0c   // SetTextColor(a0)                                    
        80059f28 01 00 04 24     _li        a0,0x1
        80059f2c 21 10 b1 03     addu       v0,sp,s1
                             LAB_80059f30                                   
        80059f30 14 01 42 80     lb         v0,0x114(v0)
        80059f34 21 28 00 00     clear      a1
        80059f38 21 90 40 00     move       s2,v0
        80059f3c 20 10 50 00     add        v0,v0,s0
        80059f40 80 10 02 00     sll        v0,v0,0x2
        80059f44 21 18 a2 03     addu       v1,sp,v0
        80059f48 40 10 10 00     sll        v0,s0,0x1
        80059f4c 20 10 50 00     add        v0,v0,s0
        80059f50 80 10 02 00     sll        v0,v0,0x2
        80059f54 20 10 50 00     add        v0,v0,s0
        80059f58 20 00 64 8c     lw         a0,0x20(v1)
        80059f5c c9 33 04 0c     jal        0x8010cf24  // RenderString(a0, a1, a2)                                   
        80059f60 01 00 46 20     _addi      a2,v0,0x1
        80059f64 01 00 22 22     addi       v0,s1,0x1
        80059f68 21 10 a2 03     addu       v0,sp,v0
        80059f6c 14 01 42 80     lb         v0,0x114(v0)
        80059f70 00 00 00 00     nop
        80059f74 22 10 52 00     sub        v0,v0,s2
        80059f78 03 00 41 04     bgez       v0,LAB_80059f88
        80059f7c 83 c8 02 00     _sra       t9,v0,0x2
        80059f80 03 00 42 24     addiu      v0,v0,0x3
        80059f84 83 c8 02 00     sra        t9,v0,0x2
                             LAB_80059f88                                   
        80059f88 00 16 19 00     sll        v0,t9,0x18
        80059f8c 1c 00 bf 8f     lw         ra,0x1c(sp)
        80059f90 18 00 b2 8f     lw         s2,0x18(sp)
        80059f94 14 00 b1 8f     lw         s1,0x14(sp)
        80059f98 10 00 b0 8f     lw         s0,0x10(sp)
        80059f9c 03 16 02 00     sra        v0,v0,0x18
        80059fa0 08 00 e0 03     jr         ra
        80059fa4 20 01 bd 27     _addiu     sp,sp,0x120
