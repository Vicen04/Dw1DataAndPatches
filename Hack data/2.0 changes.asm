// These are the changes made for the version 2.0

//New special evolution


int HandleSpecialEvolution(int input,int *EnityPtr)
{
//code ignored
    if (RandomNumber < 50) //the 50% for the evolution after sleeping
      {
      if ((DigimonLevel == 2) && (MapID == 1)) //in-training sleeping in Kunemon's bed
        EvoTarget = 32; //Kunemon
      
      else if ((DigimonLevel == 3) && (MapID == 62)) //rookie sleeping in Myotismon's bedroom
        EvoTarget = 6; //Devimon
      }
      
//code ignored
}


Disassembly:

                         HandleSpecialEvolution

         Offset       Hex         Commands  

        800e33d4 02 00 01 24     li         at,0x2
        800e33d8 0a 00 41 12     beq        s2,at,0x800e3404
        800e33dc 00 00 00 00     _nop
        800e33e0 03 00 01 24     li         at,0x3
        800e33e4 35 00 41 16     bne        s2,at,0x800e34bc
        800e33e8 00 00 00 00     _nop
        800e33ec 7c 92 82 93     lbu        v0,-0x6d84(gp) //MapID
        800e33f0 3e 00 01 24     li         at,0x3e
        800e33f4 31 00 41 14     bne        v0,at,LAB_800e34bc
        800e33f8 00 00 00 00     _nopg
        800e33fc 06 00 00 10     b          0x800e3418
        800e3400 06 00 02 24     _li        v0,0x6




//New Script functions


Script 11 (GiveRandomItem):
{
      GetByteFromScript(&itemOffset);
      GetTwoBytesFromScript(&maxRange, &quantity);
      iVar1 = ReturnRandom(maxRange);
      iVar1 = AddInventoryItem(iVar1 + itemOffset, quantity);
      if (iVar1 == 0) //checks if the item was given
        SetTrigger(0);      
      else 
        UnsetTrigger(0);      
      break;
}

Script 2E (Unload Door):
{
    GetByteFromScript(&check);
    if (check == 0) 
      UnsetObject(4009,0); //door object
    
}


Disassembly:

                             GiveRandomItem 

         Offset       Hex         Commands  
                               
        800e30f4 66 19 04 0c     jal        0x80106598  //GetByteFromScript                               
        800e30f8 24 00 a4 27     _addiu     a0,sp,0x24
        800e30fc 22 00 a4 27     addiu      a0,sp,0x22
        800e3100 a5 19 04 0c     jal        0x80106694  //GetTwoBytesFromScript                           
        800e3104 20 00 a5 27     _addiu     a1,sp,0x20
        800e3108 22 00 a4 93     lbu        a0,0x22(sp)
        800e310c b5 8d 02 0c     jal        0x800a36d4  //ReturnRandom                                    
        800e3110 00 00 00 00     _nop
        800e3114 24 00 a4 93     lbu        a0, 0x24(sp)
        800e3118 20 00 a5 93     lbu        a1,0x20(sp)
        800e311c 20 20 44 00     add        a0,v0,a0
        800e3120 90 14 03 0c     jal        0x800c5240  //AddInventoryItem                                 
        800e3124 00 00 00 00     _nop
        800e3128 05 00 40 14     bne        v0,zero,0x800e3140
        800e312c 00 00 00 00     _nop
        800e3130 70 19 04 0c     jal        0x801065c0  //SetTrigger                                      
        800e3134 21 20 00 00     _clear     a0
        800e3138 03 00 00 10     b          0x800e3148
        800e313c 00 00 00 00     _nop
                             LAB_800e3140                                  
        800e3140 7f 19 04 0c     jal        0x801065fc  //UnsetTrigger                                   
        800e3144 21 20 00 00     _clear     a0
                             LAB_800e3148                                 
        800e3148 2b 7f 00 10     b          0x80102df8
        800e314c 00 00 00 00     _nop
 
                             UnloadDoor                                   
        800e31d4 66 19 04 0c     jal        0x80106598  //GetByteFromScript                                
        800e31d8 33 00 a4 27     _addiu     a0,sp,0x33
        800e31dc 33 00 a2 83     lb         v0,0x33(sp)
        800e31e0 00 00 00 00     nop
        800e31e4 04 00 40 14     bne        v0,zero,0x800e31f8
        800e31e8 00 00 00 00     _nop
        800e31ec a9 0f 04 24     li         a0,0xfa9
        800e31f0 02 8c 02 0c     jal        0x800a3008  //UnsetObject                                   
        800e31f4 21 28 00 00     _clear     a1
                             LAB_800e31f8                                  
        800e31f8 61 0d 04 08     j          0x80103584 
        800e31fc 00 00 00 00     _nop
        800e3200 00 00 00 00     nop
        800e3204 00 00 00 00     nop




//Choose starter changes


int SetScriptForIntro()
{
    box 11:  //This one originally triggered either 8 or 9
       ShowChoiceBox(24,12); //shows a choice box and jumps to the selected box (in this case either 12 or 13)
       return 0;
    break;
    box 12: //new
       WritePStat(254,2); //Third starter
       NextBox = 17;
       return 0;
    box 13: //new
       WritePStat(254,3); //Fourth starter
       NextBox = 17;
       return 0;
}

StartSetup()
{
/code ignored

      iVar1 = ReadPStat(0xfe);

      InitialPartner = 17; //Gabumon

      if (iVar1 == 0) 
        InitialPartner = 3;  //Agumon
    
      else if (iVar1 == 3) 
      {
        Technique1 = 49; //Not the actual technique value, but the place in the digimon data
        LearnMove(19);  // Ice Needle
        InitialPartner = 4; //Betamon
      }

      else if (iVar1 == 1) 
      {
        Technique1 = 49; //Not the actual technique value, but the place in the digimon data
        LearnMove(12); //Static Elec
        InitialPartner = 18; //Elecmon
      }


//code ignored
}

                           SetScrip For Intro

	  Offset       Hex         Commands  

        8010c8d8 0c 00 05 24     _li        a1,0xc


                             box 12                                   
        800e3150 fe 00 04 24     li         a0,0xfe
        800e3154 1d 19 04 0c     jal        0x80106474  //WritePStat                                      
        800e3158 02 00 05 24     _li        a1,0x2
        800e315c 11 00 02 24     li         v0,0x11
        800e3160 ca 32 04 08     j          0x8010cb28
        800e3164 ce 94 82 a7     _sh        v0,-0x6b32(gp)  //NextBox
                             box 13                                  
        800e3168 fe 00 04 24     li         a0,0xfe
        800e316c 1d 19 04 0c     jal        0x80106474  //WritePStat                                    
        800e3170 03 00 05 24     _li        a1,0x3
        800e3174 11 00 02 24     li         v0,0x11
        800e3178 ca 32 04 08     j          0x8010cb28
        800e317c ce 94 82 a7     _sh        v0,-0x6b32(gp) //NextBox



                             SetStarter3Tech                                
        800e3180 03 00 01 24     li         at,0x3
        800e3184 08 00 22 14     bne        at,v0,0x800e31a8
        800e3188 00 00 00 00     _nop
        800e318c 31 00 04 24     li         a0,0x31
        800e3190 15 80 01 3c     lui        at,0x8015
        800e3194 ec 57 24 a0     sb         a0,0x57ec(at)                    
        800e3198 c5 97 03 0c     jal        0x800e5f14  //LearnMove                                   
        800e319c 13 00 04 24     _li        a0,0x13
        800e31a0 0e 2e 00 10     b          0x800ee9dc
        800e31a4 04 00 10 24     _li        s0,0x4
                             SetStarter4Tech                               
        800e31a8 01 00 01 24     li         at,0x1
        800e31ac 07 00 22 14     bne        at,v0,0x800e31cc
        800e31b0 00 00 00 00     _nop
        800e31b4 31 00 04 24     li         a0,0x31
        800e31b8 15 80 01 3c     lui        at,0x8015
        800e31bc ec 57 24 a0     sb         a0,0x57ec(at)                  
        800e31c0 c5 97 03 0c     jal        0x800e5f14  //LearnMove                                      
        800e31c4 0c 00 04 24     _li        a0,0xc
        800e31c8 12 00 10 24     li         s0,0x12
                             LAB_800e31cc                                   
        800e31cc 03 2e 00 10     b          0x800ee9dc
        800e31d0 00 00 00 00     _nop



//Select BGM track

Select track(ptr* BGM, Ptr* track)
{

   switch(overrideValue)
   {
      default:  //added this default option
         *track = 2;  
      case 9:
         *track = 1;
   }

}

        80105580 1a 00 20 10     beq        at,zero,LAB_801055ec

        80105668 1b 00 02 24     li         v0,0x1b
        8010566c 00 00 22 a2     sb         v0,0x0(s1)
        80105670 05 00 00 10     b          0x80105688
        80105674 00 00 00 a2     _sb        zero,0x0(s0)





//WereGarurumon nodes
                             WereGarurumonNodes
        800e3208 ff ff           dw         FFFFh
        800e320a ff 00           dw         FFh
        800e320c 00 01           dw         100h
        800e320e 01 02           dw         201h
        800e3210 02 03           dw         302h
        800e3212 03 02           dw         203h
        800e3214 04 05           dw         504h
        800e3216 05 06           dw         605h
        800e3218 06 02           dw         206h
        800e321a 07 08           dw         807h
        800e321c 08 09           dw         908h
        800e321e 09 01           dw         109h
        800e3220 0a 0b           dw         B0Ah
        800e3222 0b 0c           dw         C0Bh
        800e3224 0c 0d           dw         D0Ch
        800e3226 0d 0e           dw         E0Dh
        800e3228 0e 0b           dw         B0Eh
        800e322a 0f 10           dw         100Fh
        800e322c 10 11           dw         1110h
        800e322e 11 12           dw         1211h
        800e3230 00 00           dw         0h
        800e3232 00 00           dw         0h

