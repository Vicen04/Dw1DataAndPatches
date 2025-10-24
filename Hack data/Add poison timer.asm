//This adds a timer to the poison, burn, freeze and shock effects. It only works on 2.2 due to the code changes

//The poison timer starts at 500, which makes it last for about 25 seconds before it dissapears

TickbattleEffects(void)
{
      if (((fighter->BattleFlags & 1) != 0) && (NO_AI_FLAG == 0)) 
      {
        iVar4 = fighter->poisonTimer + -1;
        fighter->poisonTimer = iVar4;

        if (iVar4 % 100 == 0) 
        {
          if ((digimonEntity->newEffects & 3) != 0) 
          {
            damage = CalculateDamageRandom3(&digimonEntity->DigimonStats);
            iVar4 = fighter->HPDamage + damage;
            fighter->HPDamage = iVar4;

            if (9999 < iVar4) 
              fighter->HPDamage = 9999;
            
            if (currentTarget != 0) 
              CombatHead[iVar8 + 0x5c4] = 0;
            
            color = 12;
            if (entityPtr->newEffects == 2) 
              color = 3;
            
            AddEntityText(digimonEntity, currentTarget, color, damage, 0);
          }
          if (fighter->poisonTimer < 1) 
            RemovePoison(entityPtr,fighter);          
        }
      }
}

//tournament version

        80066478 48 92 82 8f     lw         v0,-0x6db8(gp) //NO_AI_FLAG
        8006647c 00 00 00 00     nop
        80066480 30 00 40 14     bne        v0,zero,0x80066544
        80066484 00 00 00 00     _nop
        80066488 1c 00 02 86     lh         v0,0x1c(s0)
        8006648c 64 00 03 24     li         v1,0x64
        80066490 ff ff 42 20     addi       v0,v0,-0x1
        80066494 1c 00 02 a6     sh         v0,0x1c(s0)
        80066498 1a 00 43 00     div        v0,v1
        8006649c 00 00 00 00     nop
        800664a0 57 00 23 82     lb         v1,0x57(s1)
        800664a4 10 10 00 00     mfhi       v0
        800664a8 26 00 40 14     bne        v0,zero,0x80066544
        800664ac 03 00 63 30     _andi      v1,v1,0x3
        800664b0 1d 00 03 10     beq        zero,v1,0x80066528
        800664b4 00 00 00 00     _nop
        800664b8 69 90 01 0c     jal        0x800641a4  //CalculateDamageRandom3                          
        800664bc 21 20 12 00     _move      a0,s2
        800664c0 21 38 02 00     move       a3,v0
        800664c4 00 1c 02 00     sll        v1,v0,0x10
        800664c8 2e 00 02 86     lh         v0,0x2e(s0)
        800664cc 03 1c 03 00     sra        v1,v1,0x10
        800664d0 20 10 43 00     add        v0,v0,v1
        800664d4 2e 00 02 a6     sh         v0,0x2e(s0)
        800664d8 10 27 41 28     slti       at,v0,0x2710
        800664dc 03 00 20 14     bne        at,zero,0x800664ec
        800664e0 00 00 00 00     _nop
        800664e4 0f 27 02 24     li         v0,0x270f
        800664e8 2e 00 02 a6     sh         v0,0x2e(s0)
                             LAB_800664ec                                   
        800664ec 05 00 60 12     beq        s3,zero,0x80066504
        800664f0 00 00 00 00     _nop
        800664f4 20 92 82 8f     lw         v0,-0x6de0(gp)               
        800664f8 00 00 00 00     nop
        800664fc 21 10 82 02     addu       v0,s4,v0
        80066500 c4 05 40 a0     sb         zero,0x5c4(v0)
                             LAB_80066504                                    
        80066504 10 00 a0 af     sw         zero,0x10(sp)
        80066508 21 20 20 02     move       a0,entityPtr
        8006650c 57 00 22 82     lb         v0,0x57(s1)
        80066510 02 00 01 24     li         at,0x2
        80066514 02 00 41 14     bne        v0,at,0x80066520
        80066518 0c 00 06 24     _li        a2,0xc
        8006651c 03 00 06 24     li         a2,0x3
                             LAB_80066520                                   
        80066520 1a 7e 03 0c     jal        0x800df868  //AddEntityText                                
        80066524 21 28 13 00     _move      a1,s3
                             LAB_80066528                                  
        80066528 1c 00 02 86     lh         v0,0x1c(s0)
        8006652c 00 00 00 00     nop
        80066530 04 00 40 1c     bgtz       v0,0x80066544
        80066534 00 00 00 00     _nop
        80066538 21 20 11 00     move       a0,entityPtr
        8006653c 36 91 01 0c     jal        0x800644d8  //RemovePoison                        
        80066540 21 28 00 02     _move      a1=>CombatAddr,s0


//The battle version is mostly the same but with different addresses for the function calls



//Added a way to remove poison in the tournaments

void RemovePoisonTournament(DigimonEntity * digimon,FighterData * fighter)

{
  BattleFlags BVar1;
  
  fighter->BattleFlags = fighter->BattleFlags & 0xfffe;
  fighter->poisonTimer = 0;
  BVar1 = fighter->BattleFlags;
  digimon->newEffects = 0;
  if (((BVar1 & 0xe) == 0) && (fighter->flattenTimer == 0)) 
    RemoveFX(digimon, &fighter, 1);
  
  return;
}

                             
                             RemovePoisonTournament                         
        800644d8 e8 ff bd 27     addiu      sp,sp,-0x18
        800644dc 10 00 bf af     sw         ra,0x10(sp)
        800644e0 21 18 a0 00     move       v1,param_2
        800644e4 34 00 62 94     lhu        v0,0x34(v1)
        800644e8 00 00 00 00     nop
        800644ec fe ff 42 30     andi       v0,v0,0xfffe
        800644f0 34 00 62 a4     sh         v0,0x34(v1)
        800644f4 1c 00 60 a4     sh         zero,0x1c(v1)
        800644f8 34 00 62 94     lhu        v0,0x34(v1)
        800644fc 00 00 00 00     nop
        80064500 0e 00 42 30     andi       v0,v0,0xe
        80064504 07 00 40 14     bne        v0,zero,0x80064524
        80064508 57 00 80 a0     _sb        zero,0x57(a0)
        8006450c 22 00 62 84     lh         v0,0x22(v1)
        80064510 00 00 00 00     nop
        80064514 03 00 40 14     bne        v0,zero,0x80064524
        80064518 00 00 00 00     _nop
        8006451c d9 98 01 0c     jal        0x80066364  //RemoveFX                                         
        80064520 01 00 06 24     _li        a2,0x1
                             LAB_80064524                                   
        80064524 10 00 bf 8f     lw         ra,0x10(sp)
        80064528 00 00 00 00     nop
        8006452c 08 00 e0 03     jr         ra
        80064530 18 00 bd 27     _addiu     sp,sp,0x18

