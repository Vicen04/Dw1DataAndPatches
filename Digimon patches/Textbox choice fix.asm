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
  DAT_801be948 = (int)_ScriptPointer; 
  _ScriptPointer = (short *)((int)_ScriptPointer + (local_1 + 1) * 2);
  uVar1 = FUN_80101688(0,(uint)DAT_ffff94ba);  

  DAT_801be956 = (short)uVar1 * 0xc + 2; 
  if (0xf0 < DAT_801be956) {
    DAT_801be956 = 0xf0;
  }
  DAT_801be94c = _ScriptPointer + 2; 
  do {
    DAT_801be94c = DAT_801be94c + 2; //keep advancing the pointer
  } while (*DAT_801be94c != 0xfe); //This was added to just point to the "close textbox" script command when you press triangle 
  if (DAT_ffff94ba == 0xff) {
    DAT_801be954 = 0;
  }
  else {
    DAT_801be954 = 0xd;
  }
  _ScriptPointer = (short *)((int)_ScriptPointer + 2); 
  DAT_ffff94bc = 0x10; 
  return;
}

//Added a trigger here to avoid errors during the Monochromon's shop minigame

MonochromonBubbleCreation()
{
  char cVar1;
  uint uVar2;
  uint uVar3;
  
  SetTrigger(0x31); //This was added to block the input of the triangle button
  uVar2 = ReadPStat(0xf7);
  uVar2 = uVar2 & 0xff;
  uVar3 = ReadPStat(0xf8);
  if ((uVar3 & 0xff) < 5) {
    cVar1 = ScriptIdToEntityId(uVar2);
    if (cVar1 != 0xff) {
      _DAT_ffff9434 = 0x1e;
      DAT_8012fdce = ((uVar3 & 0xff) << 5);
      SetObject(0x1b1,0,0,0x800fbe90);
    }
  }
  else {
    UnsetObject(0x1b1,0);
  }
  return;
}

//Remove the trigger that blocks the triangle button after the minigame has finished

Original: 
void ScriptInstructions(undefined4 param_1)
{
 switch(param 1)
 {
  case 100:
    GetNextScriptData(&local_5);
    _DAT_ffff94cc = (ushort)local_5;
    switch(local_5) 
    {
     //code ignored
     case 0x38:
       iVar8 = Earnings; //this is where your earnings gets stored during Monochromon's shop minigame
       if (Earnings < 0)  //Make sure the amount is a positive number
         iVar8 = Earnings + 255;
       
       WritePStat(0xf3,iVar8 / 256)); //store the ammount divided by 256, this will be later used to check if you can recruit or not Monochromon and the kind of dialogue you will get during the break
       WritePStat(0xf4,Earnings);
    }
    //code ignored
 }
 //code ignored
}

Changed: 
void ScriptInstructions(undefined4 param_1)
{
 switch(param 1)
 {
  case 100:
    GetNextScriptData(&local_5);
    _DAT_ffff94cc = (ushort)local_5;
    switch(local_5) 
    {
     //code ignored
     case 56:
      iVar8 = Earnings;
      if (Earnings < 0) 
        iVar8 = Earnings + 255;
      
      uVar4 = iVar8 / 256;
      WritePStat(0xf4,(char)iVar8);  //Swapped positions
      WritePStat(0xf3,uVar4);
      UnsetTrigger(0x31); //Added to unset the trigger
    }
    //code ignored
 }
 //code ignored
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
        801015c4 48 e9 23 ac     sw         v1,-0x16b8(at)//DAT_801be948
        801015c8 b0 94 82 af     sw         v0,-0x6b50(gp)
        801015cc a2 05 04 0c     jal        0x80101688  //FUN_80101688                                    
        801015d0 21 20 00 00     _clear     a0
        801015d4 1c 80 01 3c     lui        at,0x801c
        801015d8 56 e9 22 a4     sh         v0,-0x16aa(at)//DAT_801be956
        801015dc 56 e9 23 94     lhu        v1,-0x16aa(at)//DAT_801be956
        801015e0 00 00 00 00     nop
        801015e4 40 10 03 00     sll        v0,v1,0x1
        801015e8 20 10 43 00     add        v0,v0,v1
        801015ec 80 10 02 00     sll        v0,v0,0x2
        801015f0 02 00 42 20     addi       v0,v0,0x2
        801015f4 56 e9 22 a4     sh         v0,-0x16aa(at)//DAT_801be956
        801015f8 56 e9 22 94     lhu        v0,-0x16aa(at)//DAT_801be956
        801015fc 00 00 00 00     nop
        80101600 f1 00 41 2c     sltiu      at,v0,0xf1
        80101604 04 00 20 14     bne        at,zero,0x80101618
        80101608 00 00 00 00     _nop
        8010160c f0 00 02 24     li         v0,0xf0
        80101610 1c 80 01 3c     lui        at,0x801c
        80101614 56 e9 22 a4     sh         v0,-0x16aa(at)//DAT_801be956
                             LAB_80101618                                     
        80101618 b0 94 82 8f     lw         v0,-0x6b50(gp)//Script pointer
        8010161c 00 00 00 00     nop
        80101620 02 00 42 20     addi       v0,v0,0x2
        80101624 b0 94 82 af     sw         v0,-0x6b50(gp)//Script pointer
                             LAB_80101628                                   
        80101628 04 00 42 20     addi       v0,v0,0x4
        8010162c 00 00 43 84     lh         v1,0x0(v0) //Script pointer + 2
        80101630 fe 00 01 24     li         at,0xfe
        80101634 fc ff 61 14     bne        v1,at,0x80101628
        80101638 00 00 00 00     _nop
        8010163c 1c 80 01 3c     lui        at,0x801c
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



                      MonochromonBubbleCreation     
          
        800ff868 e0 ff bd 27     addiu      sp,sp,-0x20
        800ff86c 18 00 bf af     sw         ra,0x18(sp)
        800ff870 10 00 b0 af     sw         s0,0x10(sp)  //no need to store s1 since it is not being overwritten anymore
        800ff874 70 19 04 0c     jal        0x801065c0  //SetTrigger                                     
        800ff878 31 00 04 24     _li        a0,0x31
        800ff87c b8 18 04 0c     jal        0x801062e0  //ReadPStat                                  
        800ff880 f7 00 04 24     _li        a0,0xf7
        800ff884 ff 00 47 30     andi       a3,v0,0xff
        800ff888 b8 18 04 0c     jal        0x801062e0  //ReadPStat                                      
        800ff88c f8 00 04 24     _li        a0,0xf8
        800ff890 ff 00 50 30     andi       s0,v0,0xff
        800ff894 05 00 01 2e     sltiu      at,s0,0x5
        800ff898 13 00 20 10     beq        at,zero,0x800ff8e8
        800ff89c b1 01 04 24     _li        a0,0x1b1
        800ff8a0 51 08 04 0c     jal        0x80102144  //ScriptIdToEntityId                              
        800ff8a4 21 20 07 00     _move      a0,a3
        800ff8a8 ff 00 01 24     li         at,0xff
        800ff8ac 10 00 41 10     beq        v0,at,0x800ff8f0
        800ff8b0 00 00 00 00     _nop
        800ff8b4 1e 00 02 24     li         v0,0x1e
        800ff8b8 34 94 82 a7     sh         v0,-0x6bcc(gp) //,DAT_ffff9434
        800ff8bc 10 80 07 3c     lui        a3,0x8010
        800ff8c0 40 11 10 00     sll        v0,s0,0x5
        800ff8c4 13 80 01 3c     lui        at,0x8013
        800ff8c8 ce fd 22 a0     sb         v0,-0x232(at) //DAT_8012fdce
        800ff8cc b1 01 04 24     li         a0,0x1b1
        800ff8d0 21 28 00 00     clear      a1
        800ff8d4 21 30 00 00     clear      a2
        800ff8d8 d9 8b 02 0c     jal        0x800a2f64  //SetObject                                        
        800ff8dc 90 be e7 24     _addiu     a3,a3,-0x4170
        800ff8e0 04 00 00 10     b          0x800ff8f4
        800ff8e4 18 00 bf 8f     _lw        ra,0x18(sp)
                             LAB_800ff8e8                                   
        800ff8e8 02 8c 02 0c     jal        0x800a3008  //UnsetObject                                     
        800ff8ec 21 28 00 00     _clear     a1
                             LAB_800ff8f0                                 
        800ff8f0 18 00 bf 8f     lw         ra,0x18(sp)
                             LAB_800ff8f4                                  
        800ff8f4 10 00 b0 8f     lw         s0,0x10(sp)
        800ff8f8 08 00 e0 03     jr         ra
        800ff8fc 20 00 bd 27     _addiu     sp,sp,0x20


//Last change

Original:

                              case 56:
        801041f8 e0 94 82 8f     lw         v0,-0x6b20(gp)//DAT_ffff94e0
        801041fc 00 00 00 00     nop
        80104200 03 00 41 04     bgez       v0,0x80104210
        80104204 03 ca 02 00     _sra       t9,v0,0x8
        80104208 ff 00 42 24     addiu      v0,v0,0xff
        8010420c 03 ca 02 00     sra        t9,v0,0x8
                             LAB_80104210                                  
        80104210 ff 00 22 33     andi       v0,t9,0xff
        80104214 ff 00 45 30     andi       a1,v0,0xff
        80104218 1d 19 04 0c     jal        0x80106474   //WritePStat                                    
        8010421c f3 00 04 24     _li        a0,0xf3
        80104220 e0 94 82 8f     lw         v0,-0x6b20(gp)//DAT_ffff94e0
        80104224 f4 00 04 24     li         a0,0xf4
        80104228 ff 00 42 30     andi       v0,v0,0xff
        8010422c 1d 19 04 0c     jal        0x80106474  //WritePStat                                    
        80104230 ff 00 45 30     _andi      a1,v0,0xff
        80104234 4a 03 00 10     b          0x80104f60
        80104238 00 00 00 00     _nop


Changed:

                              case 56:
        801041f8 e0 94 83 8f     lw         v1,-0x6b20(gp)//DAT_ffff94e0
        801041fc 00 00 00 00     nop
        80104200 03 00 61 04     bgez       v1,0x80104210
        80104204 03 ca 03 00     _sra       t9,v1,0x8
        80104208 ff 00 63 24     addiu      v1,v1,0xff
        8010420c 03 ca 03 00     sra        t9,v1,0x8
                             LAB_80104210                                 
        80104210 21 28 03 00     move       a1,v1
        80104214 1d 19 04 0c     jal        0x80106474  //WritePStat                                     
        80104218 f4 00 04 24     _li        a0,0xf4
        8010421c ff 00 22 33     andi       v0,t9,0xff
        80104220 ff 00 45 30     andi       a1,v0,0xff
        80104224 1d 19 04 0c     jal        0x80106474  //WritePStat                                     
        80104228 f3 00 04 24     _li        a0,0xf3
        8010422c 7f 19 04 0c     jal        0x801065fc  //UnsetTrigger                                    
        80104230 31 00 04 24     _li        a0,0x31
        80104234 4a 03 00 10     b          0x80104f60
        80104238 21 18 00 00     _clear     v1
