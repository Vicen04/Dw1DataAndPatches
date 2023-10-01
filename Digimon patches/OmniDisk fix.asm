//The omni disk used to ignore the stat boost limit the digimon is supposed to have when you use an stat boost item. This is a way to fix it.


Original:
//This code ignores the limit and just makes a flat addition to the stat
void HandleBuffDisks(short? BuffDiskValue)
{
//code ignored
switch(BuffDiskValue) {
// code ignored
    case 0x12:
      BuffStat(EntityPtr,0,20,&DigimonOff,0xb,3);  
      BuffStat(EntityPtr,0,20,&DigimonDef,0xb,4);
      BuffStat(EntityPtr,0,20,&DigimonSpd,0xb,5);
//void BuffStat(int EntityPtr,int param_2(Used to render, probably to choose which digimon),uint *BuffValue,short *StatValue,short? param_5(another used to render, probably just to set an extra effect), short? StatBuffedValue)
      break;
// code ignored
 }
}

Changed:
//This code just calls the function again to apply the buffs using the singular buff disks
//This is due to the lack of lines in the code, I was unable to find a way to fit the code missing without having to rewrite a lot
void HandleBuffDisks(short? BuffDiskValue)
{
//code ignored
switch(BuffDiskValue) {
    case 0x12:
      HandleBuffDisks(0xf); //value for the Off Disk
      HandleBuffDisks(0x10); //value for the Def Disk
      HandleBuffDisks(0x11); //value for the Spd Disk
      break;
 }
}

Disassembly:

        Offset       Hex         Commands

Original:
                              switchD_800f0834::caseD_12                        
        800f0968 0b 00 02 24     li         v0,0xb
        800f096c 10 00 a2 af     sw         v0,0x10(sp)
        800f0970 03 00 02 24     li         v0,0x3
        800f0974 14 00 a2 af     sw         v0,0x14(sp)
        800f0978 13 80 01 3c     lui        at,0x8013
        800f097c 48 f3 24 8c     lw         a0,-0xcb8(at) //DAT_8012f348
        800f0980 21 28 00 00     clear      a1
        800f0984 14 00 06 24     li         a2,0x14
        800f0988 cf 73 01 0c     jal        0x8005cf3c          //BuffStat(a0, a1, a2, a3, 0x10(sp),0x14(sp))                        
        800f098c 21 38 00 02     _move      a3,s0       //Off Stat                      
        800f0990 0b 00 02 24     li         v0,0xb
        800f0994 10 00 a2 af     sw         v0,0x10(sp)
        800f0998 04 00 02 24     li         v0,0x4
        800f099c 14 00 a2 af     sw         v0,0x14(sp)
        800f09a0 13 80 01 3c     lui        at,0x8013
        800f09a4 48 f3 24 8c     lw         a0,-0xcb8(at) //DAT_8012f348
        800f09a8 21 28 00 00     clear      a1
        800f09ac 14 00 06 24     li         a2,0x14
        800f09b0 cf 73 01 0c     jal        0x8005cf3c         //BuffStat(a0, a1, a2, a3, 0x10(sp),0x14(sp))                        
        800f09b4 02 00 07 26     _addiu     a3,s0,0x2     //Def Stat                    
        800f09b8 0b 00 02 24     li         v0,0xb
        800f09bc 10 00 a2 af     sw         v0,0x10(sp)
        800f09c0 05 00 02 24     li         v0,0x5
        800f09c4 14 00 a2 af     sw         v0,0x14(sp)
        800f09c8 13 80 01 3c     lui        at,0x8013
        800f09cc 48 f3 24 8c     lw         a0,-0xcb8(at) //DAT_8012f348
        800f09d0 21 28 00 00     clear      a1
        800f09d4 14 00 06 24     li         a2,0x14
        800f09d8 cf 73 01 0c     jal        0x8005cf3c         //BuffStat(a0, a1, a2, a3, 0x10(sp),0x14(sp))                                
        800f09dc 04 00 07 26     _addiu     a3,s0,0x4  //Spd Stat                      
        800f09e0 4a 00 00 10     b          0x800f0834
        800f09e4 00 00 00 00     _nop


Changed:

                             switchD_800f0834::caseD_12                   
        800f0968 21 10 00 00     clear      v0
        800f096c 00 00 00 00     nop
        800f0970 00 00 00 00     nop
        800f0974 00 00 00 00     nop
        800f0978 00 00 00 00     nop
        800f097c 0f 00 04 24     li         a0,0xf
        800f0980 21 28 00 00     clear      a1
        800f0984 21 38 00 00     clear      a3
        800f0988 bc c1 03 0c     jal        0x800f06f0       //HandleBuffDisks(a0)                                  
        800f098c 00 00 00 00     _nop
        800f0990 00 00 00 00     nop
        800f0994 00 00 00 00     nop
        800f0998 00 00 00 00     nop
        800f099c 00 00 00 00     nop
        800f09a0 00 00 00 00     nop
        800f09a4 10 00 04 24     li         a0,0x10
        800f09a8 21 28 00 00     clear      a1
        800f09ac 00 00 00 00     nop
        800f09b0 bc c1 03 0c     jal        0x800f06f0      //HandleBuffDisks(a0)                           
        800f09b4 00 00 00 00     _nop
        800f09b8 00 00 00 00     nop
        800f09bc 00 00 00 00     nop
        800f09c0 00 00 00 00     nop
        800f09c4 00 00 00 00     nop
        800f09c8 00 00 00 00     nop
        800f09cc 11 00 04 24     li         a0,0x11
        800f09d0 21 28 00 00     clear      a1
        800f09d4 00 00 00 00     nop
        800f09d8 bc c1 03 0c     jal        0x800f06f0      //HandleBuffDisks(a0)                                
        800f09dc 00 00 00 00     _nop
        800f09e0 4a 00 00 10     b          0x800f0834
        800f09e4 00 00 00 00     _nop

