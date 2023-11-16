This patch enables a exclusive boost to a choosen move to each digimon partner you have, it does not affect the NPC 


//function with code deleted

Original:

int * LoadDigimonModelExtra(int DigimonValue,int param_2,uchar **param_3)

{
  if ((param_1 < 0) || (0xb3 < param_1)) 
    unaff_s0 = (int *)0x0;
  
  else if (param_2 == 0) 
  {
     //code ignored
  }
  else
  {
    
    //code ignored
  }
  return unaff_s0;
}

Changed:

int * LoadDigimonModelExtra(int DigimonValue,int param_2,uchar **param_3)
{
  
  if (((DigimonValue < 0) || (0xb3 < DigimonValue)) || (param_2 == 0))  //now (param_2 == 0) just returns 0, that section of he code was never used anyway
    unaff_s0 = 0;  
  else 
  {
    //code ignored
  }
  return unaff_s0;
}


Disassembly:
                        
                    LoadDigimonModelExtra

         Offset       Hex         Command 

Original:
        800a2a64 21 88 80 00     move       s1,a0
        800a2a68 04 00 20 06     bltz       s1,0x800a2a7c
        800a2a6c 21 90 c0 00     _move      s2,a2
        800a2a70 b4 00 21 2a     slti       at,s1,0xb4
        800a2a74 03 00 20 14     bne        at,zero,0x800a2a84
        800a2a78 00 00 00 00     _nop
                             LAB_800a2a7c   
        800a2a7c 18 01 00 10     b          0x800a2ee0
        800a2a80 21 10 00 00     _clear     v0


Changed:
        800a2a64 21 88 80 00     move       s1,a0
        800a2a68 06 00 20 06     bltz       s1,0x800a2a84
        800a2a6c 21 90 c0 00     _move      s2,a2
        800a2a70 b4 00 21 2a     slti       at,s1,0xb4
        800a2a74 03 00 20 10     beq        at,zero,0x800a2a84
        800a2a78 00 00 00 00     _nop
        800a2a7c 8d 00 a0 14     bne        a1,zero,0x800a2cb4
        800a2a80 00 00 00 00     _nop
                             LAB_800a2a84           
        800a2a84 16 01 00 10     b          0x800a2ee0
        800a2a88 21 10 00 00     _clear     v0



//The name says it all

Original:

int * CalculateMovementDamage(int DigimonPointer,int *DigimonID,int MoveID)

{
 iVar3 = *(DigimonID + 58); //Get the opponent defense

  if (MoveID == 45) // if the move is counter
    iVar3 = ((iVar3 * 3) / 10) // Reduce the defense a 70%
  
  if ((MoveID < 58) || (0x70 < MoveID)); //check if it is a normal move
  {
    iVar3 = *(DigimonPointer + 56) - iVar3;
    if (500 < iVar3) {
      iVar3 = 500;
    }
    if (iVar3 < -500) {
      iVar3 = -500;
    }
    iVar5 =ReturnRandom(21);   
    uVar6 = MoveDamage[MoveID]; //Get the damage of the move

    puVar7 = (((((Type1Effectivity + Type2Effectivity + Type3Effectivity) *
                     (uVar6 + (iVar3 * uVar6) / 500)) / 30) * (iVar5 + 90)) / 100);
  }
 //code ignored
 
  return puVar7;
}


Changed:

int * CalculateMovementDamage(int DigimonPointer,int *DigimonID,int MoveID)

{
 //code ignored

  iVar3 = *(DigimonID + 58); //Get the opponent defense

  if (MoveID == 45) // if the move is counter
    iVar3 = ((iVar3 * 3) / 10); Reduce the defense a 70%  
 
  if ((MoveID < 58) || (112 < MoveID)) //check if it is normal move
  {
    iVar3 = *(DigimonPointer + 56) - iVar3; //Attack - Defense
    if (500 < iVar3) //Make sure the difference is not bigger than 500
      iVar3 = 500;
    
    if (iVar3 < -500) //Make sure the difference is not lower than -500
      iVar3 = -500;    

    iVar5 = ReturnRandom(21);

// Start of AddMove
    uVar6 = MoveDamage[MoveID]; //Get the damage of the move

    if ((DigimonPointer == EntityPtr) && (MoveID == (&MovementBoostID)[*EntityPtr * 0x1c])) // check if the tech has a boost, this was added
       uVar6 = uVar6 + (&MovementBoostValue)[*EntityPtr * 0xe];

//End of AddMove

    puVar7 = (((((Type1Effectivity + Type2Effectivity + Type3Effectivity) * 
             (uVar6 + (iVar3 * uVar6) / 500)) / 30) * (iVar5 + 90)) / 100);
  }

  //code ignored

  return puVar7;
}



Disassembly:
                        
                    CalculateMovementDamage

         Offset       Hex         Command 

Original:

        8005c060 21 18 73 00     addu       v1,v1,s3
        8005c064 00 00 66 84     lh         a2,0x0(v1)


Changed:

        8005c060 a3 8a 02 0c     jal        0x800a2a8c  //AddMove                              
        8005c064 00 00 00 00     _nop


 
                               AddMove                                 
        800a2a8c f0 ff bd 27     addiu      sp,sp,-0x10
        800a2a90 00 00 00 00     nop
        800a2a94 21 18 73 00     addu       v1,v1,s3
        800a2a98 00 00 66 84     lh         a2,0x0(v1) //MoveDamage
        800a2a9c 00 00 00 00     nop
        800a2aa0 13 80 01 3c     lui        at,0x8013
        800a2aa4 48 f3 21 8c     lw         at,-0xcb8(at) //EntityPtr
        800a2aa8 00 00 00 00     nop
        800a2aac 0f 00 41 16     bne        s2,at,0x800a2aec
        800a2ab0 00 00 00 00     _nop
        800a2ab4 00 00 23 8c     lw         v1,0x0(at)
        800a2ab8 00 00 00 00     nop
        800a2abc c0 08 03 00     sll        at,v1,0x3
        800a2ac0 22 08 23 00     sub        at,at,v1
        800a2ac4 80 18 01 00     sll        v1,at,0x2
        800a2ac8 12 80 01 3c     lui        at,0x8012
        800a2acc c7 25 21 24     addiu      at,at,0x25c7
        800a2ad0 21 08 23 00     addu       at,at,v1
        800a2ad4 00 00 23 80     lb         v1,0x0(at) //TempMovementBoostID
        800a2ad8 04 00 03 16     bne        s0,v1,0x800a2aec
        800a2adc 00 00 00 00     _nop
        800a2ae0 03 00 23 84     lh         v1,0x3(at) //TempMovementBoostValue
        800a2ae4 00 00 00 00     nop
        800a2ae8 20 30 c3 00     add        a2,a2,v1		
		                     LAB_800a2aec                            
        800a2aec 08 00 e0 03     jr         ra
        800a2af0 10 00 bd 27     _addiu     sp,sp,0x10



//This function is in charge of rendering the box that shows your techniques/finisher and lets you choose a different technique based on the ones you own

Original:

void RenderMovementData(void)

{
 //code ignored

        FUN_800e5444(0,16,sVar8,3,MovementDamage,5);  //the rendering text function

// code ignored
}

Changed:

void RenderMovementData(void)
{
 //code ignored
        if ((&MovementBoostID)[*EntityPtr * 0x1c] == MovementToLoad) //Check if the tech has the boost
        {
          puVar2 = (MovementDamage + (&MovementBoostValue)[*EntityPtr * 0xe]);
          iVar3 = 7;  //change it to colour blue, Maeson has this as 16 which is a salmon colour
        }
        else {
          iVar3 = 0; //keep it colour white
        }
        FUN_800e5444(iVar3,16,sVar8,3,puVar2,5);  //the rendering text function

// code ignored
}



Dissasembly:

                     RenderDigimonTechData

         Offset       Hex         Command 

Original:

        800bc434 04 00 02 86     lh         v0,0x4(s0) //MovementDamage                       
        800bc438 21 20 00 00     clear      a0

Changed:

        800bc434 bd 8a 02 08     j          0x800a2af4
        800bc438 00 00 00 00     _nop
                                
                             LAB_800a2af4                                  
        800a2af4 04 00 02 86     lh         v0,0x4(s0) //MovementDamage  
        800a2af8 00 00 00 00     nop
        800a2afc 13 80 01 3c     lui        at,0x8013
        800a2b00 48 f3 21 8c     lw         at,-0xcb8(at) //EntityPtr
        800a2b04 00 00 00 00     nop
        800a2b08 00 00 24 8c     lw         a0,0x0(at) //EntityValue
        800a2b0c 00 00 00 00     nop
        800a2b10 c0 08 04 00     sll        at,a0,0x3
        800a2b14 22 08 24 00     sub        at,at,a0
        800a2b18 80 20 01 00     sll        a0,at,0x2
        800a2b1c 12 80 01 3c     lui        at,0x8012
        800a2b20 c7 25 21 24     addiu      at,at,0x25c7
        800a2b24 21 08 24 00     addu       at,at,a0
        800a2b28 00 00 24 80     lb         a0,0x0(at) //TempMovementBoostID
        800a2b2c 00 00 00 00     nop
        800a2b30 08 87 85 27     addiu      a1,gp,-0x78f8
        800a2b34 21 28 b3 00     addu       a1,a1,s3
        800a2b38 00 00 a5 90     lbu        a1,0x0(a1) //Tech choosen
        800a2b3c 00 00 00 00     nop
        800a2b40 07 00 85 14     bne        a0,a1,0x800a2b60
        800a2b44 00 00 00 00     _nop
        800a2b48 03 00 24 84     lh         a0,0x3(at) //TempMovementBoostValue
        800a2b4c 00 00 00 00     nop
        800a2b50 21 10 44 00     addu       v0,v0,a0
        800a2b54 07 00 04 24     li         a0,0x7
        800a2b58 02 00 00 10     b          0x800a2b64
        800a2b5c 00 00 00 00     _nop
                             LAB_800a2b60                                    
        800a2b60 21 20 00 00     clear      a0
                             LAB_800a2b64                                  
        800a2b64 0f f1 02 08     j          0x800bc43c
        800a2b68 00 00 00 00     _nop




//This is the function in charge of rendering the tech data when you are on top of a tech

Original:

void RenderTechData(int param_1)

{
//code ignored

    iVar8 = iVar8 + -100;
    FUN_800e5444(0,iVar8,27,3,MovementDamage,3); //the rendering text function

//code ignored
}

Changed:

void RenderTechData(int param_1)

{
//code ignored

    iVar8 = 0;
    if (MoveID == (&MovementBoostID)[*EntityPtr * 0x1c])  // check if the tech has the move
    {
      iVar8 = 7;
      puVar3 = (puVar3 + (&MovementBoostValue)[*EntityPtr * 0xe]);
    }
     iVar9 = iVar9 + -100;
     FUN_800e5444(iVar8,iVar9,27,3,puVar3,3); //the rendering text function

//code ignored
}




Disassembly:

                        RenderTechData

Original:

         Offset       Hex         Command 

        800bfac0 04 00 22 86     lh         v0,0x4(s1)  //MovementDamage                      
        800bfac4 9c ff 65 22     addi       a1,s3,-0x64

Changed:

        800bfac0 db 8a 02 08     j          0x800a2b6c
        800bfac4 00 00 00 00     _nop

                             LAB_800a2b6c                                    
        800a2b6c 04 00 22 86     lh         v0,0x4(s1) //MovementDamage  
        800a2b70 00 00 00 00     nop
        800a2b74 13 80 01 3c     lui        at,0x8013
        800a2b78 48 f3 21 8c     lw         at,-0xcb8(at) //EntityPtr
        800a2b7c 21 20 00 00     clear      a0
        800a2b80 00 00 25 8c     lw         a1,0x0(at) //Entity Value
        800a2b84 00 00 00 00     nop
        800a2b88 c0 08 05 00     sll        at,a1,0x3
        800a2b8c 22 08 25 00     sub        at,at,a1
        800a2b90 80 28 01 00     sll        a1,at,0x2
        800a2b94 12 80 01 3c     lui        at,0x8012
        800a2b98 c7 25 21 24     addiu      at,at,0x25c7
        800a2b9c 21 08 25 00     addu       at,at,a1
        800a2ba0 00 00 25 80     lb         a1,0x0(at) //TempMovementBoostID
        800a2ba4 00 00 00 00     nop
        800a2ba8 06 00 05 16     bne        s0,a1,0x800a2bc4
        800a2bac 00 00 00 00     _nop
        800a2bb0 07 00 04 24     li         a0,0x7
        800a2bb4 00 00 00 00     nop
        800a2bb8 03 00 25 84     lh         a1,0x3(at) //TempMovementBoostValue
        800a2bbc 00 00 00 00     nop
        800a2bc0 20 10 45 00     add        v0,v0,a1
                             LAB_800a2bc4                                     
        800a2bc4 9c ff 65 22     addi       a1,s3,-0x64
        800a2bc8 b2 fe 02 08     j          0x800bfac8



This works by reading data that is never used inside the raise data, here are my modifications (Maeson may have different data here):

All the original data has 0 as the first value (MovementBoostID) and 1200 as the second value (MovementBoostValue), I'll only show the changes

Player:

14D627FF MovementBoostID: FF (-1)
14D62802 MovementBoostValue: 00 00 (0)

Botamon:

14D6281B MovementBoostID: FF (-1)
14D6281E MovementBoostValue: 00 00 (0)

Koromon:

14D62837 MovementBoostID: FF (-1)
14D6283A MovementBoostValue: 00 00 (0)

Agumon:

14D62853 MovementBoostID: 02 (Spit Fire)
14D62856 MovementBoostValue: 64 00 (100)

Betamon:

14D6286F MovementBoostID: 14 (20, Water Blit)
14D62872 MovementBoostValue: 64 00 (100)

Greymon:

14D6288B MovementBoostID: 03 (Red Inferno)
14D6288E MovementBoostValue: 8C 00 (140)

Devimon:

14D628A7 MovementBoostID: 2D (45, Counter)
14D628AA MovementBoostValue: 73 00 (115)

Airdramon:

14D628C3 MovementBoostID: 0F (15, Hurricane)
14D628C6 MovementBoostValue: 86 00 (134)

Tyrannomon:

14D628DF MovementBoostID: 28 (40, Tremar)
14D628E2 MovementBoostValue: 7A 00 (122)

Meramon:

14D628FB MovementBoostID: 04 (Magma Bomb)
14D628FE MovementBoostValue: 79 00 (121)

Seadramon:

14D62917 MovementBoostID: 04 (Ice Needle)
14D6291A MovementBoostValue: 7C 00 (124)

Numemon:

14D62933 MovementBoostID: 36 (54, Rnd Spd Toss)
14D62936 MovementBoostValue: 16 01 (278)

MetalGreymon:

14D6294F MovementBoostID: 18 (24, Power Crane)
14D62952 MovementBoostValue: 12 01 (274)

Mamemon:

14D6296B MovementBoostID: 2E (46, Megaton Punch)
14D62952 MovementBoostValue: 18 01 (280)

Monzaemon:

14D62987 MovementBoostID: 0E (14, Confused Storm)
14D6298A MovementBoostValue: 77 01 (375)

Punimon:

14D629A3 MovementBoostID: FF (-1)
14D629A6 MovementBoostValue: 00 00 (0)

Tsunomon:

14D629BF MovementBoostID: FF (-1)
14D629C2 MovementBoostValue: 00 00 (0)

Gabumon:

14D629DB MovementBoostID: 2B (43, Sonic Jab)
14D629C2 MovementBoostValue: 64 00 (100)

Elecmon:

14D629F7 MovementBoostID: 0C (12, Static Elec)
14D629FA MovementBoostValue: 73 00 (115)

Kabuterimon:

14D62A13 MovementBoostID: 24 (36, Charm Perfume)
14D62A16 MovementBoostValue: DC 00 (220)

Angemon:

14D62A2F MovementBoostID: 0D (13, Wind Cutter)
14D62A32 MovementBoostValue: AC 00 (172)


Birdramon:

14D62B7B MovementBoostID: 01 (Prominence Beam)
14D62B7E MovementBoostValue: 9C 00 (158)

Garurumon:

14D62B97 MovementBoostID: 10 (16, Giga Freeze)
14D62B9A MovementBoostValue: 8A 00 (138)

Frigimon:

14D62BB3 MovementBoostID: 16 (22, Aurora Freeze)
14D62BB6 MovementBoostValue: AA 00 (170)


Whamon:

14D62BCF MovementBoostID: 17  (23, Tear Drop)
14D62BD2 MovementBoostValue: F0 00 (240)

Vegiemon:

14D62BEB MovementBoostID: 27 (39, Green Trap)
14D62BEE MovementBoostValue: 8C 00 (140)

SkullGreymon:

14D62C07 MovementBoostID: 30 (48, Dynamite Kick V2)
14D62C0A MovementBoostValue: 59 01 (345)

MetalMamemon:

14D62C23 MovementBoostID: 1B (27, Pulse Laser)
14D62C26 MovementBoostValue: D3 00 (211)

Vademon:

14D62C3F MovementBoostID: 1A (26, Metal Sprinter)
14D62C42 MovementBoostValue: 5E 01 (350)

Poyomon:

14D62C5B MovementBoostID: FF (-1)
14D62C5E MovementBoostValue: 00 00 (0)

Tokomon:

14D62C77 MovementBoostID: FF (-1)
14D62C7A MovementBoostValue: 00 00 (0)

Patamon:

14D62C93 MovementBoostID: 0D (13, Wind Cutter)
14D62C96 MovementBoostValue: 64 00 (100)

Kunemon:

14D62CAF MovementBoostID: 26 (38, Danger Sting)
14D62CB2 MovementBoostValue: 64 00 (100)

Unimon:

14D62CCB MovementBoostID: 30 (48, Dynamite Kick V2)
14D62CCE MovementBoostValue: 91 00 (145)

Ogremon:

14D62CE7 MovementBoostID: 2F (47, Buster Dive)
14D62CEA MovementBoostValue: 64 00 (100)

Shellmon:

14D62D03 MovementBoostID: 14 (20, Water Blit)
14D62D06 MovementBoostValue: BD 00 (189)

Centarumon:

14D62D1F MovementBoostID: 05 (Heat Laser)
14D62D22 MovementBoostValue: D8 00 (216)

Bakemon:
 
14D62D3B MovementBoostID: 0B (11, Megalo Spark)
14D62D3E MovementBoostValue: 76 00 (118)

Drimogemon:

14D62D57 MovementBoostID: 2F (47, Buster Dive)
14D62D5A MovementBoostValue: 64 00 (100)

Sukamon:

14D62D73 MovementBoostID: 38 (56, Ult Poop Hell)
14D62D76 MovementBoostValue: BC 01 (444)

Andromon:

14D62D8F MovementBoostID: 1F (31, Reverse Prog)
14D62D92 MovementBoostValue: 58 01 (344)

Giromon:

14D62DAB MovementBoostID: 1C (28, Delete Program)
14D62DAE MovementBoostValue: AA 00 (170)

Etemon:

14D62DC7 MovementBoostID: 2C (44, Dynamite Kick)
14D62DCA MovementBoostValue: 97 01 (407)

Yuramon:

14D62DE3 MovementBoostID: FF (-1)
14D62DE6 MovementBoostValue: 00 00 (0)

Tanemon:

14D62DFF MovementBoostID: FF (-1)
14D62E02 MovementBoostValue: 00 00 (0)

Biyomon:

14D62E1B MovementBoostID: 0A (10, Electric Cloud)
14D62E1E MovementBoostValue: 64 00 (100)

Palmon:

14D62E37 MovementBoostID: 25 (37, Poison Claw)
14D62E3A MovementBoostValue: 64 00 (100)

Monochromon:

14D62E53 MovementBoostID: 07 (Meltdown)
14D62E56 MovementBoostValue: C8 00 (200)

Leomon:

14D62E6F MovementBoostID: 2E (46, Megaton Punch)
14D62E72 MovementBoostValue: B4 00 (180)

Coelamon:

14D62E8B MovementBoostID: 23 (35, Insect Plague)
14D62E8E MovementBoostValue: 24 01 (292)

Kokatorimon:

14D62EA7 MovementBoostID: 09 (Spinning Shot)
14D62EAA MovementBoostValue: 6F 00 (111)

Kuwagamon:

14D62EC3 MovementBoostID: 20 (32, Poison Powder)
14D62EC6 MovementBoostValue: B7 00 (183)

Mojyamon:

14D62EDF MovementBoostID: 12 (18, Winter Blast)
14D62EE2 MovementBoostValue: E6 00 (230)

Nanimon:

14D62EFB MovementBoostID: 37 (55, Horizontal Kick)
14D62EFE MovementBoostValue: 5B 01 (347)

Megadramon:

14D62F17 MovementBoostID: 1D (29, DG Dimension)
14D62F1A MovementBoostValue: BC 00 (188)

Piximon:

14D62F33 MovementBoostID: 21 (33, Bug)
14D62F36 MovementBoostValue: C8 00 (200)

Digitamamon:

14D62F4F MovementBoostID: 0 (Fire Tower)
14D62F52 MovementBoostValue: BD 01 (445)

Penguinmon:

14D62F6B MovementBoostID: 04 (Ice Needle)
14D62F6E MovementBoostValue: 4A 00 (74)

Ninjamon:

14D62F87 MovementBoostID: 26 (38, Danger Sting)
14D62F8A MovementBoostValue: F3 00 (243)

Phoenixmon:

14D62FA3 MovementBoostID: 08 (Thunder Justice)
14D62FA6 MovementBoostValue: 72 00 (114)

H-Kabuterimon:

14D62FBF MovementBoostID: 27 (39, Green Trap)
14D62FC2 MovementBoostValue: 22 01 (290)

MegaSeadramon:

14D62FDB MovementBoostID: 11 (17, Ice Statue)
14D62FDE MovementBoostValue: B0 00 (176)

Machinedramon (only in the hack):

14D62FF7 MovementBoostID: 19 (25, All Range Beam)
14D62FFA MovementBoostValue: E3 00 (227)

Panyjamon:

14D63013 MovementBoostID: 2E (46, Megaton Punch)
14D63016 MovementBoostValue: E6 00 (230)

Gigadramon:

14D6302F MovementBoostID: 06 (Infinity Burn)
14D63032 MovementBoostValue: D4 00 (212)

MetalEtemon:

14D6304B MovementBoostID: 2C (44, Dynamite Kick)
14D6304E MovementBoostValue: C9 01 (457)