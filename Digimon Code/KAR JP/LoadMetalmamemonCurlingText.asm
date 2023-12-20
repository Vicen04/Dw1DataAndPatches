//This function loads the text when you play against Metal Mamemon in the curling minigame


int LoadMetalMamemonText(int currentTextLocation,uint currentTextValue) // currentTextLocation is used to determine which text should be loaded, currentTextValue is used to determine the line where it should be loaded
{
  char cVar1;
  uint uVar2;
  vector<int> textAddresses;
  vector<byte> textLocations;
  ptr pointerText;
  ptr pointerLoaction;
  ptr extraText1;
  ptr extraText2;
  int iVar3;
  
  pointerText = &ptr_metalMamemonText; // It is located at 8005d3cc in the code
  textAdresses = new vector<int>;
  for (int i = 0; i < 61; i++)
  {
   textAdresses.push_back(*pointerText); // value in that address
   pointerLocation = pointerText + 4;   // gets the next pointer
  }

  pointerLocation = &ptr_metalMamemonLocations; // It is located at 8005d4dc in the code

  extraText1 = &CircleText1; //This is just to render a Circle image
  extraText2 = &CircleTextMetalMamemon; //This is just to render a Circle image but with a different displacement
  
  textLocations = new vector<byte>;
  
  for (int i = 0; i < 10; i++)
  {
   textLocations.push_back(*pointerLocation); // value in that address
   pointerLocation = pointerLocation + 1;   // gets the next pointer
  }
 
  uVar2 = currentTextValue % 3;

  if (((int)currentTextValue < 0) && (currentTextValue != 0)) 
    uVar2 = uVar2 - 4;
  
  if (uVar2 == 0) 
    SetTextColor(7); // SetTextColor(byte color)
  
  else 
    SetTextColor(1);
  

  cVar1 = textLocations(currentTextLocation);
  RenderString(textAdresses[(int)cVar1 + currentTextValue], 0, currentTextValue * 13 + 1); // RenderString(stringPtr, initialXPos, yPos)

  if ((currentTextLocation == 1) && (currentTextValue == 7)) // This is just to render the circle with a red color
  {
    SetTextColor(10);
    RenderString(extraText1,0,0x5c);
  }

  if ((currentTextLocation == 2) && (currentTextValue == 3)) // circle with a red color like before
  {
    SetTextColor(10);
    RenderString(local_10,0,0x28);
  }

  iVar3 = (int)textLocations(currentTextLocation + 1) - (int)cVar1;

  if (iVar3 < 0) {
    iVar3 = iVar3 + 3;
  }
  return (iVar3 / 4);
}  
                            LoadMetalMamemonText
        8005bf54 d8 fe bd 27     addiu      sp,sp,-0x128
        8005bf58 18 00 bf af     sw         ra,0x18(sp)
        8005bf5c 14 00 b1 af     sw         s1,0x14(sp)
        8005bf60 10 00 b0 af     sw         s0,0x10(sp)
        8005bf64 21 88 80 00     move       s1,a0
        8005bf68 21 80 a0 00     move       s0,a1
        8005bf6c 06 80 18 3c     lui        t8,0x8006
        8005bf70 c8 d7 18 27     addiu      t8,t8,-0x2838 //ptr_metalMamemonText
        8005bf74 20 00 af 27     addiu      t7,sp,0x20
        8005bf78 3d 00 19 24     li         t9,0x3d
                             LAB_8005bf7c                                   
        8005bf7c 00 00 0e 8f     lw         t6,0x0(t8)                     
                                                                                            
        8005bf80 04 00 18 27     addiu      t8,t8,0x4
        8005bf84 ff ff 39 27     addiu      t9,t9,-0x1
        8005bf88 00 00 ee ad     sw         t6,0x0(t7)             
                                                                                             
        8005bf8c 04 00 ef 25     addiu      t7,t7,0x4
        8005bf90 fa ff 20 1f     bgtz       t9,0x8005bf7c
        8005bf94 00 00 00 00     _nop
        8005bf98 4c 8e 82 27     addiu      v0,gp,-0x71b4
        8005bf9c 00 00 43 8c     lw         v1,0x0(v0) //CircleText1;
        8005bfa0 04 00 42 8c     lw         v0,0x4(v0) //CircleTextMetalMamemon
        8005bfa4 14 01 a3 af     sw         v1,0x114(sp) //extraText1
        8005bfa8 18 01 a2 af     sw         v0,0x118(sp) //extraText2
        8005bfac 06 80 18 3c     lui        t8,0x8006
        8005bfb0 cc d8 18 27     addiu      t8,t8,-0x2734 //ptr_metalMamemonLocations
        8005bfb4 1c 01 af 27     addiu      t7,sp,0x11c
        8005bfb8 0a 00 19 24     li         t9,0xa
                             LAB_8005bfbc                                  
        8005bfbc 00 00 0e 83     lb         t6,0x0(t8)                         
        8005bfc0 01 00 18 27     addiu      t8,t8,0x1
        8005bfc4 ff ff 39 27     addiu      t9,t9,-0x1
        8005bfc8 00 00 ee a1     sb         t6,0x0(t7)
        8005bfcc 01 00 ef 25     addiu      t7,t7,0x1
        8005bfd0 fa ff 20 1f     bgtz       t9,0x8005bfbc
        8005bfd4 00 00 00 00     _nop
        8005bfd8 03 00 02 32     andi       v0,s0,0x3
        8005bfdc 04 00 01 06     bgez       s0,0x8005bff0
        8005bfe0 00 00 00 00     _nop
        8005bfe4 02 00 40 10     beq        v0,zero,0x8005bff0
        8005bfe8 00 00 00 00     _nop
        8005bfec fc ff 42 24     addiu      v0,v0,-0x4
                             LAB_8005bff0                                   
        8005bff0 06 00 40 14     bne        v0,zero,0x8005c00c
        8005bff4 00 00 00 00     _nop
        8005bff8 07 00 04 24     li         a0,0x7
        8005bffc ca 95 02 0c     jal        0x800a5728 // SetTextColor
        8005c000 00 00 00 00     _nop
        8005c004 04 00 00 10     b          0x8005c018
        8005c008 00 00 00 00     _nop
                             LAB_8005c00c                                      
        8005c00c 01 00 04 24     li         a0,0x1
        8005c010 ca 95 02 0c     jal        0x800a5728 // SetTextColor
        8005c014 00 00 00 00     _nop
                             LAB_8005c018                                   
        8005c018 21 10 b1 03     addu       v0,sp,s1
        8005c01c 1c 01 42 80     lb         v0,0x11c(v0)
        8005c020 00 00 00 00     nop
        8005c024 20 10 50 00     add        v0,v0,s0
        8005c028 80 10 02 00     sll        v0,v0,0x2
        8005c02c 21 18 a2 03     addu       v1,sp,v0
        8005c030 40 10 10 00     sll        v0,s0,0x1
        8005c034 20 10 50 00     add        v0,v0,s0
        8005c038 80 10 02 00     sll        v0,v0,0x2
        8005c03c 20 10 50 00     add        v0,v0,s0
        8005c040 01 00 46 20     addi       a2,v0,0x1
        8005c044 20 00 64 8c     lw         a0,0x20(v1)
        8005c048 21 28 00 00     clear      a1
        8005c04c d2 96 02 0c     jal        0x800a5b48 // RenderString
        8005c050 00 00 00 00     _nop
        8005c054 01 00 01 24     li         at,0x1
        8005c058 10 00 21 16     bne        s1,at,0x8005c09c
        8005c05c 00 00 00 00     _nop
        8005c060 07 00 01 24     li         at,0x7
        8005c064 0d 00 01 16     bne        s0,at,0x8005c09c
        8005c068 00 00 00 00     _nop
        8005c06c 0a 00 04 24     li         a0,0xa
        8005c070 ca 95 02 0c     jal        0x800a5728 // SetTextColor
        8005c074 00 00 00 00     _nop
        8005c078 40 10 10 00     sll        v0,s0,0x1
        8005c07c 20 10 50 00     add        v0,v0,s0
        8005c080 80 10 02 00     sll        v0,v0,0x2
        8005c084 20 10 50 00     add        v0,v0,s0
        8005c088 01 00 46 20     addi       a2,v0,0x1
        8005c08c 14 01 a4 8f     lw         a0,0x114(sp) //extraText1
        8005c090 21 28 00 00     clear      a1
        8005c094 d2 96 02 0c     jal        0x800a5b48 // RenderString
        8005c098 00 00 00 00     _nop
                             LAB_8005c09c                                  
        8005c09c 02 00 01 24     li         at,0x2
        8005c0a0 10 00 21 16     bne        s1,at,0x8005c0e4
        8005c0a4 00 00 00 00     _nop
        8005c0a8 03 00 01 24     li         at,0x3
        8005c0ac 0d 00 01 16     bne        s0,at,0x8005c0e4
        8005c0b0 00 00 00 00     _nop
        8005c0b4 0a 00 04 24     li         a0,0xa
        8005c0b8 ca 95 02 0c     jal        0x800a5728 // SetTextColor
        8005c0bc 00 00 00 00     _nop
        8005c0c0 40 10 10 00     sll        v0,s0,0x1
        8005c0c4 20 10 50 00     add        v0,v0,s0
        8005c0c8 80 10 02 00     sll        v0,v0,0x2
        8005c0cc 20 10 50 00     add        v0,v0,s0
        8005c0d0 01 00 46 20     addi       a2,v0,0x1
        8005c0d4 18 01 a4 8f     lw         a0,0x118(sp) extraText2
        8005c0d8 21 28 00 00     clear      a1
        8005c0dc d2 96 02 0c     jal        0x800a5b48 // RenderString
        8005c0e0 00 00 00 00     _nop
                             LAB_8005c0e4                                    
        8005c0e4 01 00 22 22     addi       v0,s1,0x1
        8005c0e8 21 10 a2 03     addu       v0,sp,v0
        8005c0ec 1c 01 43 80     lb         v1,0x11c(v0)
        8005c0f0 21 10 b1 03     addu       v0,sp,s1
        8005c0f4 1c 01 42 80     lb         v0,0x11c(v0)
        8005c0f8 00 00 00 00     nop
        8005c0fc 22 10 62 00     sub        v0,v1,v0
        8005c100 83 c8 02 00     sra        t9,v0,0x2
        8005c104 03 00 41 04     bgez       v0,0x8005c114
        8005c108 00 00 00 00     _nop
        8005c10c 03 00 42 24     addiu      v0,v0,0x3
        8005c110 83 c8 02 00     sra        t9,v0,0x2
                             LAB_8005c114                                  
        8005c114 00 16 19 00     sll        v0,t9,0x18
        8005c118 03 16 02 00     sra        v0,v0,0x18
        8005c11c 18 00 bf 8f     lw         ra,0x18(sp)
        8005c120 14 00 b1 8f     lw         s1,0x14(sp)
        8005c124 10 00 b0 8f     lw         s0,0x10(sp)
        8005c128 28 01 bd 27     addiu      sp,sp,0x128
        8005c12c 08 00 e0 03     jr         ra
        8005c130 00 00 00 00     _nop



