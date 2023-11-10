This is the code that makes possible to use an extra playable digimon in the hack, I'm just adding code to overwrite some data if WereGarurumon is selected as an evolution.
This makes possible to overwrite and use WereGarurumon pointers and data to raise/evolve that is already in the game, which is valid and useful.

This code was made to add Machinedramon in place of WereGarurumon.
Check the wiki for info about how this can be modified to include a different digimon.

//Used during an evolution, this will load the model data of the digimon your partner is evolving into

int GetNewDigimonModel(int DigimonToEvolve,int param_2,int *param_3,int **param_4,int *param_5)

{
  
  if ((DigimonToEvolve < 0) || (179 < DigimonToEvolve)) 
    iVar1 = 0;
  
  else 
  {
      //Code ignored
  }
  return iVar1;
}


Changed:

int GetNewDigimonModel(int DigimonToEvolve,int param_2,int *param_3,int **param_4,int *param_5)

{
  
  if (DigimonToEvolve == 62) //I just added this
    DigimonToEvolve = 115;  //You can change this to any digimon of your preference
  
  if ((DigimonToEvolve < 0) || (179 < DigimonToEvolve)) 
    iVar1 = 0;
  
  else {
    //code ignored
  }
  return iVar1;
}


Disassembly: 

//The changes look like a lot of code, because I had to move around a plenty of the old code to fit the new code

         Offset       Hex         Command  


                         GetNewDigimonModel
                            
Original:                           
                         
        800a2810 90 ff bd 27     addiu      sp,sp,-0x70
        800a2814 2c 00 bf af     sw         ra,0x2c(sp)
        800a2818 28 00 b4 af     sw         s4,0x28(sp)
        800a281c 24 00 b3 af     sw         s3,0x24(sp)
        800a2820 20 00 b2 af     sw         s2,0x20(sp)
        800a2824 1c 00 b1 af     sw         s1,0x1c(sp)
        800a2828 18 00 b0 af     sw         s0,0x18(sp)
        800a282c 80 00 b4 8f     lw         s4,0x80(sp)
        800a2830 21 80 80 00     move       s0,a0
        800a2834 21 88 c0 00     move       s1,a2
        800a2838 04 00 00 06     bltz       s0,0x00a284c
        800a283c 21 90 e0 00     _move      s2,a3
        800a2840 b4 00 01 2a     slti       at,s0,0xb4
        800a2844 03 00 20 14     bne        at,zero,0x800a2854
        800a2848 00 00 00 00     _nop
                             LAB_800a284c                                   
        800a284c 78 00 00 10     b          0x800a2a30
        800a2850 21 10 00 00     _clear     v0
                             LAB_800a2854                                    
        800a2854 2e 00 a0 14     bne        a1,zero,0x800a2910
        800a2858 00 00 00 00     _nop
        800a285c 13 80 03 3c     lui        v1,0x8013
        800a2860 ff ff 05 24     li         a1,-0x1
        800a2864 40 71 63 24     addiu      v1,v1,0x7140
        800a2868 0d 00 00 10     b          0x800a28a0
        800a286c 21 20 00 00     _clear     a0
                             LAB_800a2870                                    
        800a2870 00 00 62 8c     lw         v0,0x0(v1)                      
        800a2874 00 00 00 00     nop
        800a2878 03 00 40 14     bne        v0,zero,0x800a2888
        800a287c 00 00 00 00     _nop
        800a2880 05 00 00 10     b          0x800a2898
        800a2884 21 28 80 00     _move      a1,a0
                             LAB_800a2888                                     
        800a2888 18 00 62 84     lh         v0,0x18(v1)                       
        800a288c 00 00 00 00     nop
        800a2890 06 00 50 10     beq        v0,s0,0x800a28ac
        800a2894 00 00 00 00     _nop
                             LAB_800a2898                                    
        800a2898 1c 00 63 24     addiu      v1,v1,0x1c
        800a289c 01 00 84 20     addi       a0,a0,0x1
                             LAB_800a28a0                                   
        800a28a0 05 00 81 28     slti       at,a0,0x5
        800a28a4 f2 ff 20 14     bne        at,zero,0x800a2870
        800a28a8 00 00 00 00     _nop
                             LAB_800a28ac                                    
        800a28ac 05 00 01 24     li         at,0x5
        800a28b0 0d 00 81 14     bne        a0,at,0x800a28e8
        800a28b4 00 00 00 00     _nop
        800a28b8 ff ff 01 24     li         at,-0x1
        800a28bc 03 00 a1 14     bne        a1,at,0x800a28cc
        800a28c0 00 00 00 00     _nop
        800a28c4 5a 00 00 10     b          0x800a2a30
        800a28c8 21 10 00 00     _clear     v0
                             LAB_800a28cc                                
        800a28cc c0 10 05 00     sll        v0,a1,0x3
        800a28d0 22 10 45 00     sub        v0,v0,a1
        800a28d4 80 18 02 00     sll        v1,v0,0x2
        800a28d8 13 80 02 3c     lui        v0,0x8013
        800a28dc 40 71 42 24     addiu      v0,v0,0x7140
        800a28e0 21 18 43 00     addu       v1,v0,v1
        800a28e4 18 00 70 a4     sh         s0,0x18(v1)                      
                             LAB_800a28e8                                   
        800a28e8 00 00 62 8c     lw         v0,0x0(v1)                      
        800a28ec 01 00 01 24     li         at,0x1
        800a28f0 01 00 42 24     addiu      v0,v0,0x1
        800a28f4 00 00 62 ac     sw         v0,0x0(v1)                        
        800a28f8 00 00 62 8c     lw         v0,0x0(v1)                       
        800a28fc 00 00 00 00     nop
        800a2900 0b 00 41 10     beq        v0,at,0x800a2930
        800a2904 00 00 00 00     _nop
        800a2908 49 00 00 10     b          0x800a2a30
        800a290c 21 10 00 00     _clear     v0
                             LAB_800a2910                                 
        800a2910 02 00 01 24     li         at,0x2
        800a2914 06 00 a1 10     beq        a1,at,0x800a2930
        800a2918 00 00 00 00     _nop
        800a291c 03 00 01 24     li         at,0x3
        800a2920 03 00 a1 10     beq        a1,at,0x800a2930
        800a2924 00 00 00 00     _nop


Changed:

        800a2810 90 ff bd 27     addiu      sp,sp,-0x70
        800a2814 2c 00 bf af     sw         ra,0x2c(sp)
        800a2818 28 00 b4 af     sw         s4,0x28(sp)
        800a281c 24 00 b3 af     sw         s3,0x24(sp)
        800a2820 20 00 b2 af     sw         s2,0x20(sp)
        800a2824 1c 00 b1 af     sw         s1,0x1c(sp)
        800a2828 18 00 b0 af     sw         s0,0x18(sp)
        800a282c 80 00 b4 8f     lw         s4,0x80(sp)
        800a2830 3e 00 01 24     li         at,0x3e
        800a2834 02 00 24 14     bne        at,a0,0x800a2840
        800a2838 21 80 04 00     _move      s0,a0
        800a283c 73 00 10 24     li         s0,0x73         //this is the command that can be changed
                             LAB_800a2840                                  
        800a2840 04 00 00 06     bltz       s0,0x800a2854
        800a2844 21 90 07 00     _move      s2,a3
        800a2848 b4 00 01 2a     slti       at,s0,0xb4
        800a284c 03 00 20 14     bne        at,zero,0x800a285c
        800a2850 21 88 06 00     _move      s1,a2
                             LAB_800a2854                                 
        800a2854 76 00 00 10     b          0x800a2a30
        800a2858 21 10 00 00     _clear     v0
                             LAB_800a285c                                  
        800a285c 2d 00 a0 14     bne        a1,zero,0x800a2914
        800a2860 00 00 00 00     _nop
        800a2864 13 80 03 3c     lui        v1,0x8013
        800a2868 ff ff 05 24     li         a1,-0x1
        800a286c 40 71 63 24     addiu      v1,v1,0x7140
        800a2870 0d 00 00 10     b          0x800a28a8
        800a2874 21 20 00 00     _clear     a0
                             LAB_800a2878                                 
        800a2878 00 00 62 8c     lw         v0,0x0(v1)
        800a287c 00 00 00 00     nop
        800a2880 03 00 40 14     bne        v0,zero,0x800a2890
        800a2884 00 00 00 00     _nop
        800a2888 05 00 00 10     b          0x800a28a0
        800a288c 21 28 04 00     _move      a1,a0
                             LAB_800a2890                                 
        800a2890 18 00 62 84     lh         v0,0x18(v1)
        800a2894 00 00 00 00     nop
        800a2898 06 00 50 10     beq        v0,s0,0x800a28b4
        800a289c 00 00 00 00     _nop
                             LAB_800a28a0                                  
        800a28a0 1c 00 63 24     addiu      v1,v1,0x1c
        800a28a4 01 00 84 20     addi       a0,a0,0x1
                             LAB_800a28a8                                 
        800a28a8 05 00 81 28     slti       at,a0,0x5
        800a28ac f2 ff 20 14     bne        at,zero,0x800a2878
        800a28b0 00 00 00 00     _nop
                             LAB_800a28b4                                 
        800a28b4 05 00 01 24     li         at,0x5
        800a28b8 0c 00 81 14     bne        a0,at,0x800a28ec
        800a28bc ff ff 01 24     _li        at,-0x1
        800a28c0 03 00 a1 14     bne        a1,at,0x800a28d0
        800a28c4 00 00 00 00     _nop
        800a28c8 59 00 00 10     b          0x_800a2a30
        800a28cc 21 10 00 00     _clear     v0
                             LAB_800a28d0                                  
        800a28d0 c0 10 05 00     sll        v0,a1,0x3
        800a28d4 22 10 45 00     sub        v0,v0,a1
        800a28d8 80 18 02 00     sll        v1,v0,0x2
        800a28dc 13 80 02 3c     lui        v0,0x8013
        800a28e0 40 71 42 24     addiu      v0,v0,0x7140
        800a28e4 21 18 43 00     addu       v1,v0,v1
        800a28e8 18 00 70 a4     sh         s0,0x18(v1)
                             LAB_800a28ec                                  
        800a28ec 00 00 62 8c     lw         v0,0x0(v1)
        800a28f0 01 00 01 24     li         at,0x1
        800a28f4 01 00 42 24     addiu      v0,v0,0x1
        800a28f8 00 00 62 ac     sw         v0,0x0(v1)
        800a28fc 00 00 62 8c     lw         v0,0x0(v1)
        800a2900 00 00 00 00     nop
        800a2904 0a 00 41 10     beq        v0,at,0x800a2930
        800a2908 00 00 00 00     _nop
        800a290c 48 00 00 10     b          0x800a2a30
        800a2910 21 10 00 00     _clear     v0
                             LAB_800a2914                                   
        800a2914 02 00 01 24     li         at,0x2
        800a2918 05 00 a1 10     beq        a1,at,0x800a2930
        800a291c 03 00 01 24     _li        at,0x3
        800a2920 03 00 a1 10     beq        a1,at,0x800a2930
        800a2924 00 00 00 00     _nop



//This one is used to load the digimon from the data stored in the memory card 
int * LoadDigimonModel(int DigimonToLoad,int param_2)

{
  if (param_2 == 1) 
    piVar1 = 0;
  
  else if ((DigimonToLoad < 0) || (179 < DigimonToLoad))   
    piVar1 = 0;  
  else if (param_2 == 0)
  {
   // code ignored
  }
  else 
  {
   // code ignored
  }
  return piVar1;
}


int * LoadDigimonModel(int DigimonToLoad,int param_2)

{

  
  piVar1 = 0;
  if (param_2 != 1) 
  {
    if (DigimonToLoad == 62) //Added this, I had to change how the code looks to manage it in a few lines
      DigimonToLoad = 115; //You can change this to any digimon of your preference
    
    if ((-1 < DigimonToLoad) && (DigimonToLoad < 180)) 
    {
      if (param_2 == 0) 
      {
       //code ignored
      }
      else 
      {
        //code ignored
      }
    }
  }
  return piVar1;
}

Disassembly: 

//Same as before, looks like a lot because of code that had to be rearranged

         Offset       Hex         Command  

                              
                             LoadDigimonModel

Original:
                            
        800a1f68 b8 ff bd 27     addiu      sp,sp,-0x48
        800a1f6c 24 00 bf af     sw         ra,0x24(sp)
        800a1f70 20 00 b2 af     sw         s2,0x20(sp)
        800a1f74 1c 00 b1 af     sw         s1,0x1c(sp)
        800a1f78 18 00 b0 af     sw         s0,0x18(sp)
        800a1f7c 01 00 01 24     li         at,0x1
        800a1f80 03 00 a1 14     bne        a1,at,0x800a1f90
        800a1f84 21 88 80 00     _move      s1,a0
        800a1f88 14 01 00 10     b          0x800a23dc
        800a1f8c 21 10 00 00     _clear     v0
                             LAB_800a1f90                                    
        800a1f90 04 00 20 06     bltz       s1,0x800a1fa4
        800a1f94 00 00 00 00     _nop
        800a1f98 b4 00 21 2a     slti       at,s1,0xb4
        800a1f9c 03 00 20 14     bne        at,zero,0x800a1fac
        800a1fa0 00 00 00 00     _nop
                             LAB_800a1fa4                                    
        800a1fa4 0d 01 00 10     b          0x800a23dc
        800a1fa8 21 10 00 00     _clear     v0
                             LAB_800a1fac                                    
        800a1fac a3 00 a0 14     bne        a1,zero,0x800a223c
        800a1fb0 00 00 00 00     _nop
        800a1fb4 13 80 10 3c     lui        s0,0x8013
        800a1fb8 ff ff 04 24     li         a0,-0x1
        800a1fbc 40 71 10 26     addiu      s0,s0,0x7140
        800a1fc0 0d 00 00 10     b          0x800a1ff8
        800a1fc4 21 18 00 00     _clear     v1


Changed:

        800a1f68 b8 ff bd 27     addiu      sp,sp,-0x48
        800a1f6c 24 00 bf af     sw         ra,0x24(sp)
        800a1f70 20 00 b2 af     sw         s2,0x20(sp)
        800a1f74 1c 00 b1 af     sw         s1,0x1c(sp)
        800a1f78 18 00 b0 af     sw         s0,0x18(sp)
        800a1f7c 01 00 01 24     li         at,0x1
        800a1f80 16 01 a1 10     beq        a1,at,0x800a23dc
        800a1f84 21 10 00 00     _clear     v0
        800a1f88 3e 00 01 24     li         at,0x3e
        800a1f8c 02 00 24 14     bne        at,a0,0x800a1f98
        800a1f90 21 88 04 00     _move      s1,a0
        800a1f94 73 00 11 24     li         s1,0x73          //this is the command that can be changed
                             LAB_800a1f98                                    
        800a1f98 10 01 20 06     bltz       s1,0x800a23dc
        800a1f9c 00 00 00 00     _nop
        800a1fa0 b4 00 21 2a     slti       at,s1,0xb4
        800a1fa4 0d 01 20 10     beq        at,zero,0x800a23dc
        800a1fa8 00 00 00 00     _nop
        800a1fac a3 00 a0 14     bne        a1,zero,0x800a223c
        800a1fb0 00 00 00 00     _nop
        800a1fb4 13 80 10 3c     lui        s0,0x8013
        800a1fb8 ff ff 04 24     li         a0,-0x1
        800a1fbc 40 71 10 26     addiu      s0,s0,0x7140
        800a1fc0 0d 00 00 10     b          0x800a1ff8
        800a1fc4 21 18 00 00     _clear     v1





Other changes:

//Most of this is technically WereGarurumon data (or lack of data)

Audio:

14D68EC6  00 to 30  to use Megadramon sounds (only for Machinedramon)
14D68E82  07 to 02  to set this to match Megadramon (only for Machinedramon)

Model pointers:

14D5C8EC 34 3D 13 80 to DC 3E 13 80 to change WereGarurumon to Machinedramon when it has to search for the file name (only for Machinedramon)
14D5C480 00 00 00 00 to C0 CB 11 80 from no data to Machinedramon Model pointer (only for Machinedramon)


//The maeson version may use different data for the next 3 sections

Stat gains:

Original:
14D6CDCC HP: 70 17 (6000)
14D6CDCE MP: B8 0B (3000)
14D6CDD0 Offense: 2C 01 (300)
14D6CDD2 Defense: 58 02 (600)
14D6CDD4 Speed: 58 02 (600)
14D6CDD6 Brains: 58 02 (600)

Changed:
14D6CDCC HP: 61 1E (7777)
14D6CDCE MP: 61 1E (7777)
14D6CDD0 Offense: 09 03 (777)
14D6CDD2 Defense: 09 03 (777)
14D6CDD4 Speed: 09 03 (777)
14D6CDD6 Brains: 09 03 (777)


Raise data:

Original:

14D62FEC Hunger time 1: 00 
14D62FED Hunger time 2: 00 
14D62FEE Hunger time 3: 00 
14D62FEF Hunger time 4: 00 
14D62FF0 Hunger time 5: 00 
14D62FF1 Hunger time 6: FF 
14D62FF2 Hunger time 7: FF 
14D62FF3 Hunger time 8: FF 
14D62FF4 Energy capacity: 00 
14D62FF5 Energy threshold: 00 
14D62FF6 Energy usage: 00 
14D62FF7 Unknown 1: 00 (this is not used anywhere in the code, at least from what I have)
14D62FF8 Poop timer: 00 00
14D62FFA Unknown 2: 00  (this is not used anywhere in the code either, I'm actually not sure if this is a byte or it is a WORD and it should go together with the next one)
14D62FFB Unknown 3: 00
14D62FFC Poop size: 00
14D62FFD Favourite food: 00 
14D62FFE Sleep cycle: 00
14D62FFF Prefered map type: 00 
14D63000 Training type: 00 
14D63001 Default weight: 00 
14D63002 View X: 00 00 
14D63004 View Y: 00 00
14D63006 View Z: 00 00 

Changed:

14D62FEC Hunger time 1: 03 
14D62FED Hunger time 2: 06 
14D62FEE Hunger time 3: 0F (15)
14D62FEF Hunger time 4: 13 (19)
14D62FF0 Hunger time 5: 17 (23)
14D62FF1 Hunger time 6: FF 
14D62FF2 Hunger time 7: FF 
14D62FF3 Hunger time 8: FF 
14D62FF4 Energy capacity: 60 (96)
14D62FF5 Energy threshold: 10 (16)
14D62FF6 Energy usage: 07 
14D62FF7 Unknown 1: 00 (this is not used anywhere in the code, at least from what I have)
14D62FF8 Poop timer: 36 00  (54)
14D62FFA Unknown 2: B0  (this is not used anywhere in the code either, I'm actually not sure if this is a byte or it is a Half and it should go together with the next one)
14D62FFB Unknown 3: 04
14D62FFC Poop size: 0F
14D62FFD Favourite food: 43 (67, Digiseabass)
14D62FFE Sleep cycle: 04
14D62FFF Prefered map type: 00 
14D63000 Training type: 00 
14D63001 Default weight: 4D (77)
14D63002 View X: A0 FD 
14D63004 View Y: 45 02
14D63006 View Z: 00 00 


Digimon data:

Original:
14D6F8D4 Name: 57 65 72 65 47 61 72 75 72 75 6D 6F 6E 00 00 00 00 00 00 00 (WereGarurumon)
14D6F8E8 Node count: 00 00 00 00
14D6F8EC Radius: 00 00 
14D6F8EE Height: 00 00 
14D6F8F0 Type: 02 
14D6F8F1 Level: 00
14D6F8F2 Speciality 1: FF
14D6F8F3 Speciality 2: FF
14D6F8F4 Speciality 3: FF
14D6F8F5 Item drop: 00
14D6F8F6 Drop chance: 0A
14D6F8F7 Techniques: 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00

Changed:
14D6F8D4 Name: 4D 61 63 68 69 6E 65 64 72 61 6D 6F 6E 00 00 00 00 00 00 00 (Machinedramon)
14D6F8E8 Node count: 20 00 00 00
14D6F8EC Radius: 90 01
14D6F8EE Height: 58 02
14D6F8F0 Type: 03 
14D6F8F1 Level: 05
14D6F8F2 Speciality 1: 05
14D6F8F3 Speciality 2: 02
14D6F8F4 Speciality 3: 01
14D6F8F5 Item drop: 46 
14D6F8F6 Drop chance: 00
14D6F8F7 Techniques: 2E 0B 08 16 19 6A 1E 07 FF FF FF FF FF FF FF FF  (this is different from the original Machinedramon)


Enable the use of Machinedramon moves:
14D6BF30 FF FF to 73 00  (can be changed)

//This is to add some animations to Machinedramon actions: feeding, training, being happy...

00D25EB0 Machinedramon animations:

Original:
D8 00 00 00 4A 05 00 00 98 08 00 00 00 00 00 00
72 0F 00 00 00 00 00 00 00 00 00 00 00 00 00 00 
00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 
00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 
00 00 00 00 00 00 00 00 00 00 00 00 1E 15 00 00 
00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 
00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 
DE 19 00 00 00 00 00 00 00 00 00 00 00 00 00 00 
00 00 00 00 D8 00 00 00 4A 05 00 00 98 08 00 00 
72 0F 00 00 62 21 00 00 00 00 00 00 00 00 00 00 
B6 24 00 00 44 2F 00 00 0C 38 00 00 94 3F 00 00 
F8 49 00 00 00 00 00 00 BC 51 00 00 DE 58 00 00 
B4 60 00 00 FC 6B 00 00 14 77 00 00 66 7E 00 00 
00 85 00 00 44 9A 00 00

Changed:
D8 00 00 00 4A 05 00 00 98 08 00 00 98 08 00 00 
72 0F 00 00 66 7E 00 00 00 00 00 00 00 00 00 00 
DE 58 00 00 94 3F 00 00 14 77 00 00 66 7E 00 00 
BC 51 00 00 62 21 00 00 4A 05 00 00 4A 05 00 00 
4A 05 00 00 4A 05 00 00 66 7E 00 00 1E 15 00 00 
00 00 00 00 B6 24 00 00 B4 60 00 00 4A 05 00 00 
F8 49 00 00 FC 6B 00 00 00 00 00 00 72 0F 00 00 
DE 19 00 00 00 00 00 00 00 00 00 00 00 00 00 00 
00 00 00 00 D8 00 00 00 4A 05 00 00 98 08 00 00 
72 0F 00 00 62 21 00 00 00 00 00 00 00 00 00 00 
B6 24 00 00 44 2F 00 00 0C 38 00 00 94 3F 00 00 
F8 49 00 00 00 00 00 00 BC 51 00 00 DE 58 00 00 
B4 60 00 00 FC 6B 00 00 14 77 00 00 66 7E 00 00 
00 85 00 00 44 9A 00 00

