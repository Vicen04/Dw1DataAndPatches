//This patch attemps to fix an error that can happen when you press triangle to cancel a choice textbox
//This is long to explain, so I may make a wiki page for it

//For the original code, please check the repository

Changed:

//The function itself

void TextboxChoiceCreation()

{
  uint uVar1;
  byte local_1;
  
  GetNextScriptPointerData(&local_1); 
  DAT_801be950 = (ushort)local_1; 
  DAT_801be952 = 0; 
  FirstChoicePointer = _ScriptPointer; 
  _ScriptPointer = _ScriptPointer + (local_1 + 1) * 2;
  uVar1 = FUN_80101688(0,(uint)DAT_ffff94ba);  

  DAT_801be956 = (short)uVar1 * 0xc + 2; 
  if (0xf0 < DAT_801be956) {
    DAT_801be956 = 0xf0;
  }
  
  _ScriptPointer = _ScriptPointer + 2;  
  TriangleClosePointer = _ScriptPointer; 
  
   if ((*FirstChoicePointer + CurrentSectionScriptBasePointer) - (int)_ScriptPointer < 1) //If the script of the first option starts before the current script pointer
   {
    do {
      TriangleClosePointer = TriangleClosePointer + 2; //keep reading the script (without changing the script pointer)
    } while (*TriangleClosePointer != 0xfe); //get the first "close textbox, stop script" command, then set and call this when you press triangle 
   }; 
  if (DAT_ffff94ba == 0xff) 
    DAT_801be954 = 0;  
  else 
    DAT_801be954 = 0xd;
  
  DAT_ffff94bc = 0x10; 
  return;
}

Disassembly:

         Offset       Hex             Commands

                           TextboxChoiceCreation

        80101590 e0 ff bd 27     addiu      sp,sp,-0x20
        80101594 10 00 bf af     sw         ra,0x10(sp)
        80101598 66 19 04 0c     jal        0x80106598  //GoToNextScriptInstruction                       
        8010159c 1f 00 a4 27     _addiu     a0,sp,0x1f
        801015a0 1f 00 a2 93     lbu        v0,0x1f(sp)//local_1
        801015a4 1c 80 01 3c     lui        at,0x801c
        801015a8 50 e9 22 a4     sh         v0,-0x16b0(at)//DAT_801be950                   
        801015ac 01 00 42 20     addi       v0,v0,0x1
        801015b0 52 e9 20 a4     sh         zero,-0x16ae(at)//DAT_801be952  
        801015b4 b0 94 83 8f     lw         v1,-0x6b50(gp)//Script pointer 
        801015b8 40 10 02 00     sll        v0,v0,0x1
        801015bc ba 94 85 93     lbu        a1,-0x6b46(gp)//DAT_ffff94ba
        801015c0 20 10 62 00     add        v0,v1,v0
        801015c4 48 e9 23 ac     sw         v1,-0x16b8(at)//FirstChoicePointer
        801015c8 b0 94 82 af     sw         v0,-0x6b50(gp)
        801015cc a2 05 04 0c     jal        0x80101688  //FUN_80101688                                    
        801015d0 21 20 00 00     _clear     a0
        801015d4 1c 80 01 3c     lui        at,0x801c
        801015d8 56 e9 22 a4     sh         v0,-0x16aa(at) //DAT_801be956                    
        801015dc 56 e9 23 94     lhu        v1,-0x16aa(at) //DAT_801be956                   
        801015e0 00 00 00 00     nop
        801015e4 40 10 03 00     sll        v0,v1,0x1
        801015e8 20 10 43 00     add        v0,v0,v1
        801015ec 80 10 02 00     sll        v0,v0,0x2
        801015f0 02 00 42 20     addi       v0,v0,0x2
        801015f4 56 e9 22 a4     sh         v0,-0x16aa(at) //DAT_801be956                    
        801015f8 f1 00 41 2c     sltiu      at,v0,0xf1
        801015fc 03 00 20 14     bne        at,zero,0x8010160c
        80101600 1c 80 01 3c     _lui       at,0x801c
        80101604 f0 00 02 24     li         v0,0xf0
        80101608 56 e9 22 a4     sh         v0,-0x16aa(at) //DAT_801be956                    
                             LAB_8010160c                                     
        8010160c b0 94 82 8f     lw         v0,-0x6b50(gp) //ScriptPointer
        80101610 48 e9 23 8c     lw         v1,-0x16b8(at) //FirstChoicePointer                    
        80101614 02 00 42 24     addiu      v0,v0,0x2
        80101618 ac 94 84 8f     lw         a0,-0x6b54(gp) //CurrentSectionScriptBasePointer
        8010161c 00 00 63 84     lh         v1,0x0(v1) //value of the FirstChoicePointer 
        80101620 b0 94 82 af     sw         v0,-0x6b50(gp) //ScriptPointer
        80101624 20 18 64 00     add        v1,v1,a0
        80101628 22 18 62 00     sub        v1,v1,v0
        8010162c 06 00 60 1c     bgtz       v1,0x80101648
        80101630 00 00 00 00     _nop
                             LAB_80101634                                     
        80101634 02 00 42 24     addiu      v0,v0,0x2
        80101638 00 00 43 84     lh         v1,0x0(v0) //TriangleClosePointer + 2 
        8010163c fe 00 01 24     li         at,0xfe
        80101640 fc ff 61 14     bne        v1,at,0x80101634
        80101644 1c 80 01 3c     _lui       at,0x801c
                             LAB_80101648                                   
        80101648 4c e9 22 ac     sw         v0,-0x16b4(at) //TriangleClosePointer                  
        8010164c ff 00 01 24     li         at,0xff
        80101650 ba 94 82 93     lbu        v0,-0x6b46(gp) //DAT_ffff94ba
        80101654 ff 00 01 24     li         at,0xff
        80101658 04 00 41 10     beq        v0,at,0x8010166c
        8010165c 1c 80 01 3c     _lui       at,0x801c
        80101660 0d 00 02 24     li         v0,0xd
        80101664 02 00 00 10     b          0x80101670
        80101668 54 e9 22 a4     _sh        v0,-0x16ac(at) //DAT_801be954                    
                             LAB_8010166c                                     
        8010166c 54 e9 20 a4     sh         zero,-0x16ac(at) //DAT_801be954                   
                             LAB_80101670                                    
        80101670 10 00 02 24     li         v0,0x10
        80101674 bc 94 82 a3     sb         v0,-0x6b44(gp)//DAT_ffff94bc
        80101678 10 00 bf 8f     lw         ra,0x10(sp)
        8010167c 00 00 00 00     nop
        80101680 08 00 e0 03     jr         ra
        80101684 20 00 bd 27     _addiu     sp,sp,0x20

         
