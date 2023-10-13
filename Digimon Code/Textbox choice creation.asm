//This function is in charge of the creation of textboxes that have multiple options to choose from
//I was unsure about how to name the variables, so I left them with just the addresses

void TextboxChoiceCreation()

{
  uint uVar1;
  byte local_1;
  
  GetNextScriptPointerData(&local_1); //Gets the next script pointer data and stores it inside local_1 in this case

  DAT_801be950 = (ushort)local_1; //This is the amount of options this choice box has

  DAT_801be952 = 0; //Current choice, this sets it to the default which is 0, when you choose a different one, it will change in a different part of the code

  DAT_801be948 = ScriptPointer; //Stores the current script pointer

  ScriptPointer = ScriptPointer + (local_1 + 1) * 2;  //Advances the script based on the amount of options, this will set the script just at the start of the first option

  uVar1 = FUN_80101688(0,(uint)DAT_ffff94ba); //Reads the script text and stores it in memory

  DAT_801be956 = (short)uVar1 * 0xc + 2; // Probably the size based on the amount of text

  if (0xf0 < DAT_801be956) //I guess this is a failsafe
    DAT_801be956 = 0xf0;
  
  if (DAT_ffff94ba == 0xff)
    DAT_801be954 = 0;
  else 
    DAT_801be954 = 0xd;
  
  DAT_801be94c = ScriptPointer + 2; //stores the place where it should move the script in case triangle is pressed 
//note that this is just the next script instruction after the start of the first choice, which can cause a lot of errors when this should just close the box

  ScriptPointer = ScriptPointer + 2; //Advance the script

  DAT_ffff94bc = 0x10; //unsure
  return;
}


Disassembly:

         Offset       Hex             Commands
                             
        80101590 e0 ff bd 27     addiu      sp,sp,-0x20
        80101594 10 00 bf af     sw         ra,0x10(sp)
        80101598 66 19 04 0c     jal        0x80106598 //GetNextScriptPointerData                                     
        8010159c 1f 00 a4 27     _addiu     a0,sp,0x1f
        801015a0 1f 00 a2 93     lbu        v0,0x1f(sp)//local_1
        801015a4 1c 80 01 3c     lui        at,0x801c
        801015a8 50 e9 22 a4     sh         v0,-0x16b0(at)//DAT_801be950                    
        801015ac 1c 80 01 3c     lui        at,0x801c
        801015b0 01 00 42 20     addi       v0,v0,0x1
        801015b4 52 e9 20 a4     sh         zero,-0x16ae(at)//DAT_801be952                   
        801015b8 b0 94 83 8f     lw         v1,-0x6b50(gp)//Script instruction
        801015bc 40 10 02 00     sll        v0,v0,0x1
        801015c0 1c 80 01 3c     lui        at,0x801c
        801015c4 20 10 62 00     add        v0,v1,v0
        801015c8 ba 94 85 93     lbu        a1,-0x6b46(gp)//DAT_ffff94ba
        801015cc 48 e9 23 ac     sw         v1,-0x16b8(at)//DAT_801be948                    
        801015d0 b0 94 82 af     sw         v0,-0x6b50(gp)//Script instruction
        801015d4 a2 05 04 0c     jal        0x80101688 //FUN_80101688                                    
        801015d8 21 20 00 00     _clear     a0
        801015dc 1c 80 01 3c     lui        at,0x801c
        801015e0 56 e9 22 a4     sh         v0,-0x16aa(at)//DAT_801be956                    
        801015e4 1c 80 01 3c     lui        at,0x801c
        801015e8 56 e9 23 94     lhu        v1,-0x16aa(at)//DAT_801be956                 
        801015ec 00 00 00 00     nop
        801015f0 40 10 03 00     sll        v0,v1,0x1
        801015f4 20 10 43 00     add        v0,v0,v1
        801015f8 80 10 02 00     sll        v0,v0,0x2
        801015fc 02 00 42 20     addi       v0,v0,0x2
        80101600 1c 80 01 3c     lui        at,0x801c
        80101604 56 e9 22 a4     sh         v0,-0x16aa(at)//DAT_801be956                  
        80101608 1c 80 01 3c     lui        at,0x801c
        8010160c 56 e9 22 94     lhu        v0,-0x16aa(at)//DAT_801be956                   
        80101610 00 00 00 00     nop
        80101614 f1 00 41 2c     sltiu      at,v0,0xf1
        80101618 04 00 20 14     bne        at,zero,0x8010162c
        8010161c 00 00 00 00     _nop
        80101620 f0 00 02 24     li         v0,0xf0
        80101624 1c 80 01 3c     lui        at,0x801c
        80101628 56 e9 22 a4     sh         v0,-0x16aa(at)//DAT_801be956                 
                             LAB_8010162c                                    
        8010162c b0 94 82 8f     lw         v0,-0x6b50(gp)//Script instruction
        80101630 1c 80 01 3c     lui        at,0x801c
        80101634 02 00 42 20     addi       v0,v0,0x2
        80101638 b0 94 82 af     sw         v0,-0x6b50(gp)//Script instruction
        8010163c b0 94 82 8f     lw         v0,-0x6b50(gp)//Script instruction
        80101640 00 00 00 00     nop
        80101644 4c e9 22 ac     sw         v0,-0x16b4(at)//DAT_801be94c                    
        80101648 ba 94 82 93     lbu        v0,-0x6b46(gp)//DAT_ffff94ba
        8010164c ff 00 01 24     li         at,0xff
        80101650 05 00 41 10     beq        v0,at,0x80101668
        80101654 00 00 00 00     _nop
        80101658 0d 00 02 24     li         v0,0xd
        8010165c 1c 80 01 3c     lui        at,0x801c
        80101660 03 00 00 10     b          0x80101670
        80101664 54 e9 22 a4     _sh        v0,-0x16ac(at)//DAT_801be954             
                             LAB_80101668                                  
        80101668 1c 80 01 3c     lui        at,0x801c
        8010166c 54 e9 20 a4     sh         zero,-0x16ac(at)//DAT_801be954                
                             LAB_80101670                                   
        80101670 10 00 02 24     li         v0,0x10
        80101674 bc 94 82 a3     sb         v0,-0x6b44(gp)//DAT_ffff94bc
        80101678 10 00 bf 8f     lw         ra,0x10(sp)
        8010167c 00 00 00 00     nop
        80101680 08 00 e0 03     jr         ra
        80101684 20 00 bd 27     _addiu     sp,sp,0x20