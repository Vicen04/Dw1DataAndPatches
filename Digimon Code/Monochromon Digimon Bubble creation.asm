//This is the code used to set up the bubbles that the digimon use to show their opinion about the prices you offer to the during the Monochromon's shop minigame

void MonochromonBubbleCreation()

{
  char cVar1;
  uint uVar2;
  uint uVar3;
  
  uVar2 = ReadPStat(0xf7); //Entity
  uVar3 = ReadPStat(0xf8); //type of face (0 is the happiest, 4 is the angriest; 5 is used to not render it)
  if ((uVar3 & 0xff) < 5) //Check if the value is valid
  {
   cVar1 = ScriptIdToEntityId(uVar2 & 0xff); //Makes sure the entity is valid
    if (cVar1 != 0xff) 
    {
      _DAT_ffff9434 = 0x1e;
      DAT_8012fdce = ((uVar3 & 0xff) << 5); //Store the type of face
      SetObject(0x1b1,0,0,0x800fbe90); //Sets the bubble
    }
  }
  else 
    UnsetObject(0x1b1,0); //Destroys the bubble
  
  return;
}


Disassembly:

         Offset       Hex             Commands

        800ff868 e0 ff bd 27     addiu      sp,sp,-0x20
        800ff86c 18 00 bf af     sw         ra,0x18(sp)
        800ff870 14 00 b1 af     sw         s1,0x14(sp)
        800ff874 10 00 b0 af     sw         s0,0x10(sp)
        800ff878 b8 18 04 0c     jal        0x801062e0  //ReadPStat                                   
        800ff87c f7 00 04 24     _li        a0,0xf7
        800ff880 ff 00 51 30     andi       s1,v0,0xff
        800ff884 b8 18 04 0c     jal        0x801062e0  //ReadPStat                               
        800ff888 f8 00 04 24     _li        a0,0xf8
        800ff88c ff 00 50 30     andi       s0,v0,0xff
        800ff890 05 00 01 2e     sltiu      at,s0,0x5
        800ff894 13 00 20 10     beq        at,zero,0x800ff8e4
        800ff898 b1 01 04 24     _li        a0,0x1b1
        800ff89c 51 08 04 0c     jal        0x80102144  //ScriptIdToEntityId                             
        800ff8a0 21 20 20 02     _move      a0,s1
        800ff8a4 ff 00 01 24     li         at,0xff
        800ff8a8 10 00 41 10     beq        v0,at,0x800ff8ec
        800ff8ac 00 00 00 00     _nop
        800ff8b0 1e 00 02 24     li         v0,0x1e
        800ff8b4 34 94 82 a7     sh         v0,-0x6bcc(gp) //DAT_ffff9434
        800ff8b8 10 80 07 3c     lui        a3,0x8010
        800ff8bc 40 11 10 00     sll        v0,s0,0x5
        800ff8c0 13 80 01 3c     lui        at,0x8013
        800ff8c4 ce fd 22 a0     sb         v0,-0x232(at)//DAT_8012fdce                 
        800ff8c8 b1 01 04 24     li         a0,0x1b1
        800ff8cc 21 28 00 00     clear      a1
        800ff8d0 21 30 00 00     clear      a2
        800ff8d4 d9 8b 02 0c     jal        0x800a2f64   //SetObject                               
        800ff8d8 90 be e7 24     _addiu     a3,a3,-0x4170 //0x800fbe90
        800ff8dc 04 00 00 10     b          0x800ff8f0
        800ff8e0 18 00 bf 8f     _lw        ra,0x18(sp)
                             LAB_800ff8e4                                  
        800ff8e4 02 8c 02 0c     jal        0x800a3008  //UnsetObject                                 
        800ff8e8 21 28 00 00     _clear     a1
                             LAB_800ff8ec                                   
        800ff8ec 18 00 bf 8f     lw         ra,0x18(sp)
                             LAB_800ff8f0                                    
        800ff8f0 14 00 b1 8f     lw         s1,0x14(sp)
        800ff8f4 10 00 b0 8f     lw         s0,0x10(sp)
        800ff8f8 08 00 e0 03     jr         ra
        800ff8fc 20 00 bd 27     _addiu     sp,sp,0x20
