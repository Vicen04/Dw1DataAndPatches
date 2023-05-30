void EvolutionCode(int entityPtr,short *StatsPtr,int DigimonDataPtr,int targetDigimon) // Used to get and set the new digimon data during the evolution
{
  
  OffsetSum = targetDigimon * 14;
  currentDigimonLevel = (&DigimonLevel)[DigimonType * 52];
  targetDigimonTempValue = (&DataFromStatGains)[targetDigimon * 7];
  if ((targetDigimon == 11) // Numemon || (targetDigimon == 39) // Sukamon || (targetDigimon == 53)) // Nanimon ||(targetDigimon == 6) // Devimon 
  || (CurrentDigimon == 39) // current digimon is Sukamon || (targetDigimonLevel < 3) // less level than a rookie)
  {
    SpecialStatGains(StatsPtr,(&StatGainsBrainsPtr + OffsetSum));
    targetDigimonTempValue = (&DataFromStatGains)[targetDigimon * 7];
  }
  else {
    if (EvoItemFlag == 0) // checks if an evo item was used
     {
       tempMaxHP = StatsPtr[8]; // StatsPtr[8] is where the MaxHP data is located
       StatsGainsHP = *(&StatsGainsHpPtr + OffsetSum);
       if (tempMaxHP < StatsGainsHP)
       {
         StatsGainsHP = StatsGainsHP + tempMaxHP;
         tempMaxHP = StatsGainsHP / 2;
        if (StatsGainsHP < 0)         
          tempMaxHP = (StatsGainsHP + 1) / 2;
         
        StatsPtr[8] = tempMaxHP; 
       }
       else 
         StatsPtr[8] = tempMaxHP + (StatsGainsHP / 10);
       
      tempMaxMP = StatsPtr[9]; // StatsPtr[9] is where the MaxMP data is located
      StatsGainsMP = *(&StatGainsMpPtr + OffsetSum);
      if (tempMaxMP < StatsGainsMP)
       {
        StatsGainsMP = StatsGainsMP + tempMaxMP;
        tempMaxMP = StatsGainsMP / 2;
        if (StatsGainsMP < 0) 
          tempMaxMP = (StatsGainsMP + 1) / 2;  
      
        StatsPtr[9] = tempMaxMP;
       }
      else 
        StatsPtr[9] = tempMaxMP + (StatsGainsMP / 10);
      
      tempOffense = *StatsPtr; // pointer where the Offense data is located
      StatsGainsOff = *(&StatGainsOffPtr + OffsetSum);
      if (tempOffense < StatsGainsOff) 
      {
        StatsGainsOff = StatsGainsOff + tempOffense;
        tempOffense = StatsGainsOff / 2;
        if (StatsGainsOff < 0) 
          tempOffense = (StatsGainsOff + 1)/ 2;
        
        *StatsPtr = tempOffense;
      }
      else 
        *StatsPtr = tempOffense + (StatsGainsOff / 10);
      
      tempDefense = StatsPtr[1]; // StatsPtr[1] is where the Defense data is located
      StatsGainsDef = *(&StatGainsDefPtr + OffsetSum);
      if (tempDefense < StatsGainsDef)
      {
        StatsGainsDef = StatsGainsDef + tempDefense;
        tempDefense = StatsGainsDef / 2;
        if (StatsGainsDef < 0) 
          tempDefense = (StatsGainsDef + 1) / 2;
        
        StatsPtr[1] = tempDefense;
      }
      else 
        StatsPtr[1] = tempDefense + (StatsGainsDef / 10);
      
      tempSpeed = StatsPtr[2]; // StatsPtr[2] is where the Speed data is located
      StatsGainsSpeed = *(&StatGainsSpeedPtr + OffsetSum);
      if (tempSpeed < StatsGainsSpeed) 
      {
        StatsGainsSpeed = StatsGainsSpeed + tempSpeed;
        tempSpeed = StatsGainsSpeed / 2;
        if (StatsGainsSpeed < 0) 
          tempSpeed = (StatsGainsSpeed + 1) / 2;
        
        StatsPtr[2] = tempSpeed;
      }
      else 
        StatsPtr[2] = tempSpeed + (StatsGainsSpeed / 10);
      
      tempBrains = StatsPtr[3]; // StatsPtr[3] is where the Brains data is located
      StatsGainsBrains = *(&StatGainsBrainsPtr + OffsetSum);
      if (tempBrains < StatsGainsBrains)
      {
        StatsGainsBrains = StatsGainsBrains + tempBrains;
        tempBrains = StatsGainsBrains / 2;
        if (StatsGainsBrains < 0) 
          tempBrains = (StatsGainsBrains + 1) / 2;
        
        StatsPtr[3] = tempBrains;
      }
      else 
        StatsPtr[3] = tempBrains + (StatsGainsBrains / 10);
      
      CheckLimits();
      if (scriptIsRunning == 1) {
        if ((&DigimonLevel)[targetDigimonTempValue * 52] == 4) {
          AddTamerLevel(20,1); // 20% chance to get a Tamer level
        }
        if ((&DigimonLevel)[targetDigimonTempValue * 52] == 5) {
          AddTamerLevel(100,1); // 100% chance to get a Tamer level
        }
      }
    }
    targetDigimonTempValue = (&DataFromStatGains)[targetDigimon * 7];
  }
  OffsetToTargetDigimonValues = targetDigimonTempValue * 52;

  Weight = (&DefaultWeightList)[targetDigimonTempValue * 28]; // Gets the default weight of the target digimon and sets it as the new weight

  DigimonCareMistakes = 0; // Sets the care mistakes to 0

  DigimonBattles = 0; // Sets the battles to 0

  if (((&DigimonLevel)[OffsetToTargetDigimonValues] == 5) && (evoItemFlag == 0)) 
    Lifetime = Lifetime + 96; // if the target digimon is an ultimate and an item was not used, add 4 days (96 hours) to the lifetime
  
  TargetDigimonMainElement = (&DigimonElementValues)[OffsetToTargetDigimonValues];

  if (TargetDigimonMainElement == 0) //fire 
    MovementOffset = 0;
  else if (TargetDigimonMainElement == 1) // battle
    MovementOffset5 = 40;
  else if (TargetDigimonMainElement == 2) // wind
    MovementOffset = 8;
  else if (TargetDigimonMainElement == 3) // earth
    MovementOffset = 32;
  else if (TargetDigimonMainElement == 4) // ice
    MovementOffset = 16;
  else if (TargetDigimonMainElement == 5) // mech
    MovementOffset = 24;
  else if (TargetDigimonMainElement == 6) // filth
    MovementOffset = 49;
  
  targetDigimonTechniquesPointer = &PointerToDigimonTechniques + OffsetToTargetDigimonValues;
  count = 0;
  
  tempVector = new Vector<byte>; //This is just something I added to make it easier to understand, it is actually an array made with local data
  tempPointer = targetDigimonTechniquesPointer;
  for (i = 0; i < 16; i++) 
  {
    if ((MovementOffset <= *tempPointer) && (*tempPointer <= (MovementOffset + 8))) {
      tempVector.add(*tempPointer);
      count++;
    }
    tempPointer = tempPointer + 1;
  }
  tempVector[count] = -1;
  MoveId = tempVector[0];
  for (i = 0; tempMove = tempVector[i], tempMove != -1; i++) 
    {
    if ((*((&TechniquesDamage + tempMove * 16) < *(&TechniquesDamage + MoveId * 16)) &&
       (*(&TechniquesDamage + tempMove * 16) != 0))  // checks the damage of the techniques and sets the least damaging one
      MoveId = tempMove;
    
  }
  LearnMove(MoveId);
  temp = 0;
  while ((temp < 16 && (SelectedMove = *targetDigimonTechniquesPointer, targetDigimonTechniquesPointer = targetDigimonTechniquesPointer + 1, SelectedMove != MoveId))) {
    temp = temp + 1;
  }
  Technique1 = temp + 46;
  Technique2 = -1;
  Technique3 = -1;
  if ((byte)(&DigimonLevel)[OffsetToTargetDigimonValues] < 3) 
    Technique1 = 46; //If the level is lower than a rookie, set the movement to the first tech
  
  count = 0xf;
  do {
     techValue = (&PointerToDigimonTechniques)[count + OffsetToTargetDigimonValues];
     if (((finisherValue != 0xff) && (0x39 < techValue)) && (techValue < 0x71)) {
      Finisher = count + 46;
     break;
    
    count--;
     } while(count >= 0 );
      if (count < 0) {
        Finisher = 0xff;
      }
      tempDigimonPosRotData = *(entityPtr + 4);
      newDigimonPositionX = *(tempDigimonPosRotData + 120);
      newDigimonPositionY = *(tempDigimonPosRotData + 124);
      newDigimonPositionZ = *(tempDigimonPosRotData + 128);
      newDigimonRotationX = *(tempDigimonPosRotData + 112);
      newDigimonRotationY = *(tempDigimonPosRotData + 116);
      newDigimonRotationZ = *(tempDigimonPosRotData + 114);

      FUN_800a1430(DigimonType,1); // Delete old digimon model
      EntityPtr = 0;

      thunk_FUN_800a23f4(DigimonType,3); // Free memory and data?

      EvolvedDigimonDataSetup?(targetDigimonTempValue,newDigimonPositionX,newDigimonPositionY,newDigimonPositionZ,newDigimonRotationX,newDigimonRotationY,newDigimonRotationZ);
      SetDigimonAdquiredTrigger(targetDigimonTempValue & 0xffff); 

      if (currentDigimonLevel != (&DigimonLevel)[OffsetToTargetDigimonValues]) // restart the evolution timer if the digimon level is different
        DigimonEvoTimer = 0;

      return;
    }
   

}


Disassembly:

        Offset       Hex         Commands
                            
        80063350 a0 ff bd 27     addiu      sp,sp,-0x60
        80063354 44 00 bf af     sw         ra,0x44(sp)
        80063358 40 00 be af     sw         s8,0x40(sp)
        8006335c 3c 00 b7 af     sw         s7,0x3c(sp)
        80063360 38 00 b6 af     sw         s6,0x38(sp)
        80063364 34 00 b5 af     sw         s5,0x34(sp)
        80063368 30 00 b4 af     sw         s4,0x30(sp)
        8006336c c0 10 07 00     sll        v0,a3,0x3
        80063370 2c 00 b3 af     sw         s3,0x2c(sp)
        80063374 28 00 b2 af     sw         s2,0x28(sp)
        80063378 24 00 b1 af     sw         s1,0x24(sp)
        8006337c 21 a0 c0 00     move       s4,a2
        80063380 13 80 06 3c     lui        a2,0x8013
        80063384 21 b0 80 00     move       s6,a0
        80063388 22 10 47 00     sub        v0,v0,a3
        8006338c 40 20 02 00     sll        a0,v0,0x1
        80063390 13 80 02 3c     lui        v0,0x8013
        80063394 d0 b2 42 24     addiu      v0,v0,-0x4d30
        80063398 21 88 44 00     addu       s1,v0,a0
        8006339c 15 80 01 3c     lui        at,0x8015
        800633a0 a8 57 22 8c     lw         v0,0x57a8(at) // DigimonType
        800633a4 20 00 b0 af     sw         s0,0x20(sp)
        800633a8 40 20 02 00     sll        a0,v0,0x1
        800633ac 20 20 82 00     add        a0,a0,v0
        800633b0 80 20 04 00     sll        a0,a0,0x2
        800633b4 20 20 82 00     add        a0,a0,v0
        800633b8 80 20 04 00     sll        a0,a0,0x2
        800633bc d1 ce c6 24     addiu      a2,a2,-0x312f
        800633c0 21 20 c4 00     addu       a0,a2,a0
        800633c4 00 00 84 90     lbu        a0,0x0(a0)
        800633c8 21 18 e0 00     move       v1,a3
        800633cc 00 24 04 00     sll        a0,a0,0x10
        800633d0 4c 00 a4 af     sw         a0,local_14(sp)
        800633d4 4c 00 a4 8f     lw         a0,local_14(sp)
        800633d8 0c 00 30 86     lh         s0,0xc(s1)
        800633dc 03 24 04 00     sra        a0,a0,0x10
        800633e0 4c 00 a4 af     sw         a0,0x4c(sp)
        800633e4 40 20 03 00     sll        a0,v1,0x1
        800633e8 20 20 83 00     add        a0,a0,v1
        800633ec 80 20 04 00     sll        a0,a0,0x2
        800633f0 20 18 83 00     add        v1,a0,v1
        800633f4 80 18 03 00     sll        v1,v1,0x2
        800633f8 21 18 c3 00     addu       v1,a2,v1
        800633fc 00 00 63 90     lbu        v1,0x0(v1)
        80063400 0b 00 01 24     li         at,0xb
        80063404 00 1c 03 00     sll        v1,v1,0x10
        80063408 10 00 e1 10     beq        a3,at,0x8006344c // LAB_8006344c
        8006340c 03 1c 03 00     _sra       v1,v1,0x10
        80063410 27 00 01 24     li         at,0x27
        80063414 0d 00 e1 10     beq        a3,at,0x8006344c // LAB_8006344c
        80063418 00 00 00 00     _nop
        8006341c 35 00 01 24     li         at,0x35
        80063420 0a 00 e1 10     beq        a3,at,0x8006344c // LAB_8006344c
        80063424 00 00 00 00     _nop
        80063428 06 00 01 24     li         at,0x6
        8006342c 07 00 e1 10     beq        a3,at,0x8006344c // LAB_8006344c
        80063430 00 00 00 00     _nop
        80063434 27 00 01 24     li         at,0x27
        80063438 04 00 41 10     beq        v0,at,0x8006344c // LAB_8006344c
        8006343c 00 00 00 00     _nop
        80063440 03 00 61 28     slti       at,v1,0x3
        80063444 0b 00 20 10     beq        at,zero,0x80063474 // LAB_80063474
        80063448 00 00 00 00     _nop
                             LAB_8006344c          
        8006344c 0a 00 22 86     lh         v0,0xa(s1)
        80063450 21 20 a0 00     move       a0,a1
        80063454 00 16 02 00     sll        v0,v0,0x18
        80063458 03 16 02 00     sra        v0,v0,0x18
        8006345c 21 28 40 00     move       a1,v0
        80063460 ca 8e 01 0c     jal        0x80063b28   //  SpecialEvolution          
        80063464 21 30 e0 00     _move      a2,a3
        80063468 0c 00 30 86     lh         s0,0xc(s1)
        8006346c bd 00 00 10     b          0x80063764  // LAB_80063764
        80063470 c0 10 10 00     _sll       v0,s0,0x3
                             LAB_80063474             
        80063474 26 93 82 83     lb         v0,-0x6cda(gp)
        80063478 00 00 00 00     nop
        8006347c b6 00 40 14     bne        v0,zero,0x80063758 // LAB_80063758
        80063480 00 00 00 00     _nop
        80063484 10 00 a4 84     lh         a0,0x10(a1)
        80063488 00 00 22 86     lh         v0,0x0(s1)
        8006348c 21 18 80 00     move       v1,a0
        80063490 21 38 60 00     move       a3,v1
        80063494 2a 08 62 00     slt        at,v1,v0
        80063498 0d 00 20 14     bne        at,zero,0x800634d0 // LAB_800634d0
        8006349c 21 30 40 00     _move      a2,v0
        800634a0 66 66 02 3c     lui        v0,0x6666
        800634a4 67 66 42 34     ori        v0,v0,0x6667
        800634a8 18 00 46 00     mult       v0,a2
        800634ac c2 1f 06 00     srl        v1,a2,0x1f
        800634b0 10 10 00 00     mfhi       v0
        800634b4 83 10 02 00     sra        v0,v0,0x2
        800634b8 21 10 43 00     addu       v0,v0,v1
        800634bc 00 14 02 00     sll        v0,v0,0x10
        800634c0 03 14 02 00     sra        v0,v0,0x10
        800634c4 20 10 82 00     add        v0,a0,v0
        800634c8 07 00 00 10     b          0x800634e8 // LAB_800634e8
        800634cc 10 00 a2 a4     _sh        v0,0x10(a1)
                             LAB_800634d0                                     
        800634d0 20 10 c7 00     add        v0,a2,a3
        800634d4 03 00 41 04     bgez       v0,0x800634e4 // LAB_800634e8
        800634d8 43 c8 02 00     _sra       t9,v0,0x1
        800634dc 01 00 42 24     addiu      v0,v0,0x1
        800634e0 43 c8 02 00     sra        t9,v0,0x1
                             LAB_800634e4                                      
        800634e4 10 00 b9 a4     sh         t9,0x10(a1)
                             LAB_800634e8                                   
        800634e8 12 00 a4 84     lh         a0,0x12(a1)
        800634ec 02 00 22 86     lh         v0,0x2(s1)
        800634f0 21 18 80 00     move       v1,a0
        800634f4 21 38 60 00     move       a3,v1
        800634f8 2a 08 62 00     slt        at,v1,v0
        800634fc 0d 00 20 14     bne        at,zero,0x80063534 // LAB_80063534
        80063500 21 30 40 00     _move      a2,v0
        80063504 66 66 02 3c     lui        v0,0x6666
        80063508 67 66 42 34     ori        v0,v0,0x6667
        8006350c 18 00 46 00     mult       v0,a2
        80063510 c2 1f 06 00     srl        v1,a2,0x1f
        80063514 10 10 00 00     mfhi       v0
        80063518 83 10 02 00     sra        v0,v0,0x2
        8006351c 21 10 43 00     addu       v0,v0,v1
        80063520 00 14 02 00     sll        v0,v0,0x10
        80063524 03 14 02 00     sra        v0,v0,0x10
        80063528 20 10 82 00     add        v0,a0,v0
        8006352c 07 00 00 10     b          0x8006354c  // LAB_8006354c
        80063530 12 00 a2 a4     _sh        v0,0x12(a1)
                             LAB_80063534                                     
        80063534 20 10 c7 00     add        v0,a2,a3
        80063538 03 00 41 04     bgez       v0,0x80063548 // LAB_80063548
        8006353c 43 c8 02 00     _sra       t9,v0,0x1
        80063540 01 00 42 24     addiu      v0,v0,0x1
        80063544 43 c8 02 00     sra        t9,v0,0x1
                             LAB_80063548                                   
        80063548 12 00 b9 a4     sh         t9,0x12(a1)
                             LAB_8006354c                                    
        8006354c 00 00 a4 84     lh         a0,0x0(a12)
        80063550 04 00 22 86     lh         v0,0x4(s1)
        80063554 21 18 80 00     move       v1,a0
        80063558 21 38 60 00     move       a3,v1
        8006355c 2a 08 62 00     slt        at,v1,v0
        80063560 0d 00 20 14     bne        at,zero,0x80063598  // LAB_80063598
        80063564 21 30 40 00     _move      a2,v0
        80063568 66 66 02 3c     lui        v0,0x6666
        8006356c 67 66 42 34     ori        v0,v0,0x6667
        80063570 18 00 46 00     mult       v0,a2
        80063574 c2 1f 06 00     srl        v1,a2,0x1f
        80063578 10 10 00 00     mfhi       v0
        8006357c 83 10 02 00     sra        v0,v0,0x2
        80063580 21 10 43 00     addu       v0,v0,v1
        80063584 00 14 02 00     sll        v0,v0,0x10
        80063588 03 14 02 00     sra        v0,v0,0x10
        8006358c 20 10 82 00     add        v0,a0,v0
        80063590 07 00 00 10     b          0x800635b0  // LAB_800635b0
        80063594 00 00 a2 a4     _sh        v0,0x0(a1)
                             LAB_80063598                                     
        80063598 20 10 c7 00     add        v0,a2,a3
        8006359c 03 00 41 04     bgez       v0,0x800635ac // LAB_800635ac
        800635a0 43 c8 02 00     _sra       t9,v0,0x1
        800635a4 01 00 42 24     addiu      v0,v0,0x1
        800635a8 43 c8 02 00     sra        t9,v0,0x1
                             LAB_800635ac                                    
        800635ac 00 00 b9 a4     sh         t9,0x0(a1)
                             LAB_800635b0                                     
        800635b0 02 00 a4 84     lh         a0,0x2(a1)
        800635b4 06 00 22 86     lh         v0,0x6(s1)
        800635b8 21 18 80 00     move       v1,a0
        800635bc 21 38 60 00     move       a3,v1
        800635c0 2a 08 62 00     slt        at,v1,v0
        800635c4 0d 00 20 14     bne        at,zero,0x800635fc // LAB_800635fc
        800635c8 21 30 40 00     _move      a2,v0
        800635cc 66 66 02 3c     lui        v0,0x6666
        800635d0 67 66 42 34     ori        v0,v0,0x6667
        800635d4 18 00 46 00     mult       v0,a2
        800635d8 c2 1f 06 00     srl        v1,a2,0x1f
        800635dc 10 10 00 00     mfhi       v0
        800635e0 83 10 02 00     sra        v0,v0,0x2
        800635e4 21 10 43 00     addu       v0,v0,v1
        800635e8 00 14 02 00     sll        v0,v0,0x10
        800635ec 03 14 02 00     sra        v0,v0,0x10
        800635f0 20 10 82 00     add        v0,a0,v0
        800635f4 07 00 00 10     b          0x80063614 // LAB_80063614 
        800635f8 02 00 a2 a4     _sh        v0,0x2(a1)
                             LAB_800635fc                                    
        800635fc 20 10 c7 00     add        v0,a2,a3
        80063600 03 00 41 04     bgez       v0,0x80063610 // LAB_80063610
        80063604 43 c8 02 00     _sra       t9,v0,0x1
        80063608 01 00 42 24     addiu      v0,v0,0x1
        8006360c 43 c8 02 00     sra        t9,v0,0x1
                             LAB_80063610                                   
        80063610 02 00 b9 a4     sh         t9,0x2(a1)
                             LAB_80063614                                    
        80063614 04 00 a4 84     lh         param_1,0x4(a1)
        80063618 08 00 22 86     lh         v0,0x8(s1)
        8006361c 21 18 80 00     move       v1,a0
        80063620 21 38 60 00     move       a3,v1
        80063624 2a 08 62 00     slt        at,v1,v0
        80063628 0d 00 20 14     bne        at,zero,0x80063660 // LAB_80063660
        8006362c 21 30 40 00     _move      a2,v0
        80063630 66 66 02 3c     lui        v0,0x6666
        80063634 67 66 42 34     ori        v0,v0,0x6667
        80063638 18 00 46 00     mult       v0,a2
        8006363c c2 1f 06 00     srl        v1,a2,0x1f
        80063640 10 10 00 00     mfhi       v0
        80063644 83 10 02 00     sra        v0,v0,0x2
        80063648 21 10 43 00     addu       v0,v0,v1
        8006364c 00 14 02 00     sll        v0,v0,0x10
        80063650 03 14 02 00     sra        v0,v0,0x10
        80063654 20 10 82 00     add        v0,a0,v0
        80063658 07 00 00 10     b          0x80063678 // LAB_80063678
        8006365c 04 00 a2 a4     _sh        v0,0x4(a1)
                             LAB_80063660                                     
        80063660 20 10 c7 00     add        v0,a2,a3
        80063664 03 00 41 04     bgez       v0,0x80063674  // LAB_80063674
        80063668 43 c8 02 00     _sra       t9,v0,0x1
        8006366c 01 00 42 24     addiu      v0,v0,0x1
        80063670 43 c8 02 00     sra        t9,v0,0x1
                             LAB_80063674                                    
        80063674 04 00 b9 a4     sh         t9,0x4(a1)
                             LAB_80063678                                   
        80063678 06 00 a4 84     lh         a0,0x6(a1)
        8006367c 0a 00 22 86     lh         v0,0xa(s1)
        80063680 21 18 80 00     move       v1,a0
        80063684 21 38 60 00     move       a3,v1
        80063688 2a 08 62 00     slt        at,v1,v0
        8006368c 0d 00 20 14     bne        at,zero,0x800636c4 // LAB_800636c4
        80063690 21 30 40 00     _move      a2,v0
        80063694 66 66 02 3c     lui        v0,0x6666
        80063698 67 66 42 34     ori        v0,v0,0x6667
        8006369c 18 00 46 00     mult       v0,a2
        800636a0 c2 1f 06 00     srl        v1,a2,0x1f
        800636a4 10 10 00 00     mfhi       v0
        800636a8 83 10 02 00     sra        v0,v0,0x2
        800636ac 21 10 43 00     addu       v0,v0,v1
        800636b0 00 14 02 00     sll        v0,v0,0x10
        800636b4 03 14 02 00     sra        v0,v0,0x10
        800636b8 20 10 82 00     add        v0,a0,v0
        800636bc 07 00 00 10     b          0x800636dc // LAB_800636dc
        800636c0 06 00 a2 a4     _sh        v0,0x6(a1)
                             LAB_800636c4                                   
        800636c4 20 10 c7 00     add        v0,a2,a3
        800636c8 03 00 41 04     bgez       v0,0x800636d8  // LAB_800636d8
        800636cc 43 c8 02 00     _sra       t9,v0,0x1
        800636d0 01 00 42 24     addiu      v0,v0,0x1
        800636d4 43 c8 02 00     sra        t9,v0,0x1
                             LAB_800636d8                                     
        800636d8 06 00 b9 a4     sh         t9,0x6(a1)
                             LAB_800636dc                                  
        800636dc 8c 8f 01 0c     jal        0x80063e30     // Check Limits                             
        800636e0 00 00 00 00     _nop
        800636e4 c8 94 82 8f     lw         v0,-0x6b38(gp)
        800636e8 01 00 01 24     li         at,0x1
        800636ec 1a 00 41 14     bne        v0,at,0x80063758 // LAB_80063758
        800636f0 00 00 00 00     _nop
        800636f4 40 10 10 00     sll        v0,s0,0x1
        800636f8 20 10 50 00     add        v0,v0,s0
        800636fc 80 10 02 00     sll        v0,v0,0x2
        80063700 20 10 50 00     add        v0,v0,s0
        80063704 80 18 02 00     sll        v1,v0,0x2
        80063708 13 80 02 3c     lui        v0,0x8013
        8006370c d1 ce 42 24     addiu      v0,v0,-0x312f
        80063710 21 10 43 00     addu       v0,v0,v1
        80063714 00 00 42 90     lbu        v0,0x0(v0)
        80063718 04 00 01 24     li         at,0x4
        8006371c 04 00 41 14     bne        v0,at,0x80063730 // LAB_80063730
        80063720 21 80 60 00     _move      s0,v1
        80063724 14 00 04 24     li         a0,0x14
        80063728 fd b2 02 0c     jal        0x800acbf4    // Add Tamer Level
        8006372c 01 00 05 24     _li        a1,0x1
                             LAB_80063730                                   
        80063730 13 80 02 3c     lui        v0,0x8013
        80063734 d1 ce 42 24     addiu      v0,v0,-0x312f
        80063738 21 10 50 00     addu       v0,v0,s0
        8006373c 00 00 42 90     lbu        v0,0x0(v0)
        80063740 05 00 01 24     li         at,0x5
        80063744 04 00 41 14     bne        v0,at,0x80063758 // LAB_80063758
        80063748 00 00 00 00     _nop
        8006374c 64 00 04 24     li         a0,0x64
        80063750 fd b2 02 0c     jal        0x800acbf4   // Add Tamer Level
        80063754 01 00 05 24     _li        a1,0x1
                             LAB_80063758                                   
        80063758 0c 00 30 86     lh         s0,0xc(s1)
        8006375c 00 00 00 00     nop
        80063760 c0 10 10 00     sll        v0,s0,0x3
                             LAB_80063764                                     
        80063764 22 10 50 00     sub        v0,v0,s0
        80063768 80 18 02 00     sll        v1,v0,0x2
        8006376c 12 80 02 3c     lui        v0,0x8012
        80063770 d1 25 42 24     addiu      v0,v0,0x25d1
        80063774 21 10 43 00     addu       v0,v0,v1
        80063778 00 00 42 90     lbu        v0,0x0(v0) // Default Weight List
        8006377c 05 00 01 24     li         at,0x5
        80063780 42 00 82 a6     sh         v0,0x42(s4)  // Weight
        80063784 40 10 10 00     sll        v0,s0,0x1
        80063788 20 10 50 00     add        v0,v0,s0
        8006378c 80 10 02 00     sll        v0,v0,0x2
        80063790 20 10 50 00     add        v0,v0,s0
        80063794 80 18 02 00     sll        v1,v0,0x2
        80063798 13 80 02 3c     lui        v0,0x8013
        8006379c 52 00 80 a6     sh         zero,0x52(s4)  // Care Mistakes
        800637a0 d1 ce 42 24     addiu      v0,v0,-0x312f
        800637a4 54 00 80 a6     sh         zero,0x54(s4)  // Battles
        800637a8 21 10 43 00     addu       v0,v0,v1
        800637ac 00 00 42 90     lbu        v0,0x0(v0)  // Target Level
        800637b0 00 00 00 00     nop
        800637b4 09 00 41 14     bne        v0,at,0x800637dc // LAB_800637dc
        800637b8 21 90 60 00     _move      s2,v1
        800637bc 26 93 82 83     lb         v0,-0x6cda(gp)  // Evo Item Flag
        800637c0 00 00 00 00     nop
        800637c4 05 00 40 14     bne        v0,zero,0x800637dc  // LAB_800637dc
        800637c8 00 00 00 00     _nop
        800637cc 48 00 82 86     lh         v0,0x48(s4)  // Lifetime
        800637d0 00 00 00 00     nop
        800637d4 60 00 42 20     addi       v0,v0,0x60
        800637d8 48 00 82 a6     sh         v0,0x48(s4)
                             LAB_800637dc                                     
        800637dc 13 80 02 3c     lui        v0,0x8013
        800637e0 d2 ce 42 24     addiu      v0,v0,-0x312e
        800637e4 21 10 52 00     addu       v0,v0,s2
        800637e8 00 00 42 90     lbu        v0,0x0(v0)
        800637ec 00 00 00 00     nop
        800637f0 03 00 40 14     bne        v0,zero,0x80063800 // LAB_80063800
        800637f4 00 00 00 00     _nop
        800637f8 1e 00 00 10     b          0x80063874  // LAB_80063874
        800637fc 21 a8 00 00     _clear     s5 
                             LAB_80063800                                     
        80063800 01 00 01 24     li         at,0x1
        80063804 03 00 41 14     bne        v0,at,0x80063814 // LAB_80063814
        80063808 00 00 00 00     _nop
        8006380c 19 00 00 10     b          0x80063874 // LAB_80063874
        80063810 28 00 15 24     _li        s5,0x28
                             LAB_80063814                                   
        80063814 02 00 01 24     li         at,0x2
        80063818 03 00 41 14     bne        v0,at,0x80063828 // LAB_80063828
        8006381c 00 00 00 00     _nop
        80063820 14 00 00 10     b          0x80063874 // LAB_80063874
        80063824 08 00 15 24     _li        s5,0x8
                             LAB_80063828                                    
        80063828 03 00 01 24     li         at,0x3
        8006382c 03 00 41 14     bne        v0,at,0x8006383c // LAB_8006383c
        80063830 00 00 00 00     _nop
        80063834 0f 00 00 10     b          0x80063874 // LAB_80063874
        80063838 20 00 15 24     _li        s5,0x20
                             LAB_8006383c                                    
        8006383c 04 00 01 24     li         at,0x4
        80063840 03 00 41 14     bne        v0,at,0x80063850 // LAB_80063850
        80063844 00 00 00 00     _nop
        80063848 0a 00 00 10     b          0x80063874 // LAB_80063874
        8006384c 10 00 15 24     _li        s5,0x10
                             LAB_80063850                                    
        80063850 05 00 01 24     li         at,0x5
        80063854 03 00 41 14     bne        v0,at,0x80063864 // LAB_80063864
        80063858 00 00 00 00     _nop
        8006385c 05 00 00 10     b          0x80063874 // LAB_80063874
        80063860 18 00 15 24     _li        s5,0x18
                             LAB_80063864                                    
        80063864 06 00 01 24     li         at,0x6
        80063868 02 00 41 14     bne        v0,at,0x80063874 // LAB_80063874
        8006386c 00 00 00 00     _nop
        80063870 31 00 15 24     li         s5,0x31
                             LAB_80063874                             
        80063874 13 80 02 3c     lui        v0,0x8013
        80063878 b4 ce 42 24     addiu      v0,v0,-0x314c
        8006387c 21 10 52 00     addu       v0,v0,s2
        80063880 23 00 44 24     addiu      a0,v0,0x23
        80063884 21 98 80 00     move       s3,a0
        80063888 21 30 00 00     clear      a2
        8006388c 21 28 00 00     clear      a1
        80063890 0e 00 00 10     b          0x800638cc // LAB_800638cc
        80063894 08 00 a3 22     _addi      v1,s5,0x8
                             LAB_80063898                                   
        80063898 00 00 82 90     lbu        v0,0x0(a0)
        8006389c 00 00 00 00     nop
        800638a0 2b 08 55 00     sltu       at,v0,s5
        800638a4 07 00 20 14     bne        at,zero,0x800638c4 // LAB_800638c4
        800638a8 21 38 40 00     _move      a3,v0
        800638ac 2a 08 67 00     slt        at,v1,a3
        800638b0 04 00 20 14     bne        at,zero,0x800638c4 // LAB_800638c4
        800638b4 00 00 00 00     _nop
        800638b8 21 10 a6 03     addu       v0,sp,a2
        800638bc 50 00 47 a0     sb         a3,0x50(v0)
        800638c0 01 00 c6 20     addi       a2,a2,0x1
                             LAB_800638c4                                    
        800638c4 01 00 84 24     addiu      a0,a0,0x1
        800638c8 01 00 a5 20     addi       a1,a1,0x1
                             LAB_800638cc                                    
        800638cc 10 00 a1 28     slti       at,a1,0x10
        800638d0 f1 ff 20 14     bne        at,zero,0x80063898 // LAB_80063898
        800638d4 00 00 00 00     _nop
        800638d8 21 10 a6 03     addu       v0,sp,a1
        800638dc ff 00 03 24     li         v1,0xff
        800638e0 50 00 43 a0     sb         v1,0x50(v0)
        800638e4 50 00 b1 93     lbu        s1,0x50(sp)
        800638e8 11 00 00 10     b          0x80063930 // LAB_80063930
        800638ec 21 30 00 00     _clear     a2
                             LAB_800638f0                                  
        800638f0 12 80 04 3c     lui        a0,0x8012
        800638f4 00 11 11 00     sll        v0,s1,0x4
        800638f8 40 62 84 24     addiu      a0,a0,0x6240
        800638fc 21 10 82 00     addu       v0,a0,v0
        80063900 00 00 43 84     lh         v1,0x0(v0)  // Move Damage
        80063904 00 11 05 00     sll        v0,a1,0x4
        80063908 21 10 82 00     addu       v0,a0,v0
        8006390c 00 00 42 84     lh         v0,0x0(v0) // Move Damage
        80063910 00 00 00 00     nop
        80063914 2a 08 43 00     slt        at,v0,v1
        80063918 04 00 20 10     beq        at,zero,0x8006392c // LAB_8006392c
        8006391c 21 20 40 00     _move      a0,v0
        80063920 02 00 80 10     beq        a0,zero,0x8006392c // LAB_8006392c
        80063924 00 00 00 00     _nop
        80063928 ff 00 b1 30     andi       s1,a1,0xff
                             LAB_8006392c                                 
        8006392c 01 00 c6 20     addi       a2,a2,0x1
                             LAB_80063930                             
        80063930 21 10 a6 03     addu       v0,sp,a2
        80063934 50 00 42 90     lbu        v0,0x50(v0)
        80063938 ff 00 01 24     li         at,0xff
        8006393c ec ff 41 14     bne        v0,at,0x800638f0 // LAB_800638f0
        80063940 21 28 40 00     _move      a1,v0
        80063944 c5 97 03 0c     jal        0x800e5f14 // Learn Move
        80063948 21 20 20 02     _move      a0,s1
        8006394c 21 20 60 02     move       a0,s3
        80063950 06 00 00 10     b          0x8006396c // LAB_8006396c
        80063954 21 28 00 00     _clear     a1
                             LAB_80063958                                    
        80063958 00 00 82 90     lbu        v0,0x0(a0)
        8006395c 01 00 83 24     addiu      v1,a0,0x1
        80063960 05 00 51 10     beq        v0,s1,0x80063978 // LAB_80063978
        80063964 21 20 60 00     _move      a0,v1
        80063968 01 00 a5 20     addi       a1,a1,0x1
                             LAB_8006396c                                  
        8006396c 10 00 a1 28     slti       at,a1,0x10
        80063970 f9 ff 20 14     bne        at,zero,0x80063958 // LAB_80063958
        80063974 00 00 00 00     _nop
                             LAB_80063978                                 
        80063978 2e 00 a2 20     addi       v0,a1,0x2e
        8006397c 15 80 01 3c     lui        at,0x8015
        80063980 ec 57 22 a0     sb         v0,0x57ec(at)  // Technique 1
        80063984 ff 00 02 24     li         v0,0xff
        80063988 15 80 01 3c     lui        at,0x8015
        8006398c ed 57 22 a0     sb         v0,0x57ed(at) // Technique 2
        80063990 15 80 01 3c     lui        at,0x8015
        80063994 ee 57 22 a0     sb         v0,0x57ee(at) // Technique 3
        80063998 13 80 02 3c     lui        v0,0x8013
        8006399c d1 ce 42 24     addiu      v0,v0,-0x312f
        800639a0 21 10 52 00     addu       v0,v0,s2
        800639a4 00 00 42 90     lbu        v0,0x0(v0) // Digimon Level
        800639a8 00 00 00 00     nop
        800639ac 03 00 41 2c     sltiu      at,v0,0x3
        800639b0 04 00 20 10     beq        at,zero,0x800639c4 // LAB_800639c4
        800639b4 00 00 00 00     _nop
        800639b8 2e 00 02 24     li         v0,0x2e
        800639bc 15 80 01 3c     lui        at,0x8015
        800639c0 ec 57 22 a0     sb         v0,0x557ec(at) // Technique 1
                             LAB_800639c4                                   
        800639c4 40 10 10 00     sll        v0,s0,0x1
        800639c8 20 10 50 00     add        v0,v0,s0
        800639cc 80 10 02 00     sll        v0,v0,0x2
        800639d0 20 10 50 00     add        v0,v0,s0
        800639d4 0f 00 05 24     li         a1,0xf
        800639d8 14 00 00 10     b          0x80063a2c // LAB_80063a2c
        800639dc 80 18 02 00     _sll       v1,v0,0x2
                             LAB_800639e0                                     
        800639e0 13 80 02 3c     lui        v0,0x8013
        800639e4 d7 ce 42 24     addiu      v0,v0,-0x3129
        800639e8 21 10 43 00     addu       v0,v0,v1
        800639ec 21 10 a2 00     addu       v0,a1,v0
        800639f0 00 00 42 90     lbu        v0,0x0(v0)
        800639f4 ff 00 01 24     li         at,0xff
        800639f8 0b 00 41 10     beq        v0,at,0x80063a28 // LAB_80063a28
        800639fc 21 20 40 00     _move      a0,v0
        80063a00 3a 00 81 2c     sltiu      at,a0,0x3a
        80063a04 08 00 20 14     bne        at,zero,0x80063a28 // LAB_80063a28
        80063a08 00 00 00 00     _nop
        80063a0c 71 00 81 2c     sltiu      at,a0,0x71
        80063a10 05 00 20 10     beq        at,zero,0x80063a28 // LAB_80063a28
        80063a14 00 00 00 00     _nop
        80063a18 2e 00 a2 20     addi       v0,a1,0x2e
        80063a1c 15 80 01 3c     lui        at,0x8015
        80063a20 04 00 00 10     b          0x80063a34 // LAB_80063a34
        80063a24 ef 57 22 a0     _sb        v0,0x57ef(at)  // Finisher
                             LAB_80063a28                                  
        80063a28 ff ff a5 20     addi       a1,a1,-0x1
                             LAB_80063a2c                                    
        80063a2c ec ff a1 04     bgez       a1,0x800639e0 // LAB_800639e0
        80063a30 00 00 00 00     _nop
                             LAB_80063a34                                    
        80063a34 04 00 a1 04     bgez       a1,0x80063a48 // LAB_80063a48
        80063a38 00 00 00 00     _nop
        80063a3c ff 00 02 24     li         v0,0xff
        80063a40 15 80 01 3c     lui        at,0x8015
        80063a44 ef 57 22 a0     sb         v0,0x57ef(at) // Finisher
                             LAB_80063a48                                  
        80063a48 04 00 c3 8e     lw         v1,0x4(s6)
        80063a4c 15 80 01 3c     lui        at,0x8015
        80063a50 78 00 62 8c     lw         v0,0x78(v1)
        80063a54 a8 57 36 8c     lw         s6,0x57a8(at) // DigimonType Ptr
        80063a58 00 ac 02 00     sll        s5,v0,0x10
        80063a5c 7c 00 62 8c     lw         v0,0x7c(v1)
        80063a60 72 00 7e 84     lh         s8,0x72(v1)
        80063a64 00 9c 02 00     sll        s3,v0,0x10
        80063a68 80 00 62 8c     lw         v0,0x80(v1)
        80063a6c 74 00 77 84     lh         s7,0x74(v1)
        80063a70 00 8c 02 00     sll        s1,v0,0x10
        80063a74 70 00 62 84     lh         v0,0x70(v1)
        80063a78 03 ac 15 00     sra        s5,s5,0x10
        80063a7c 03 9c 13 00     sra        s3,s3,0x10
        80063a80 03 8c 11 00     sra        s1,s1,0x10
        80063a84 48 00 a2 af     sw         v0,0x48(sp)
        80063a88 21 20 c0 02     move       a0,s6
        80063a8c 0c 85 02 0c     jal        0x800a1430  // Delete old digimon
        80063a90 01 00 05 24     _li        a1,0x1
        80063a94 13 80 01 3c     lui        at,0x8013
        80063a98 48 f3 20 ac     sw         zero,-0xcb8(at) // Entity Ptr
        80063a9c 21 20 c0 02     move       a0,s6
        80063aa0 98 83 02 0c     jal        0x800a0e60  // free memory and data?
        80063aa4 03 00 05 24     _li        a1,0x3
        80063aa8 48 00 a2 8f     lw         v0,0x48(sp)
        80063aac 21 20 00 02     move       a0,s0
        80063ab0 10 00 a2 af     sw         v0,0x10(sp)
        80063ab4 14 00 be af     sw         s8,0x14(sp)
        80063ab8 18 00 b7 af     sw         s7,0x18(sp)
        80063abc 21 28 a0 02     move       a1,s5
        80063ac0 21 30 60 02     move       a2,s3
        80063ac4 dd 93 02 0c     jal        0x800a4f74 // Evolved Digimon Data Setup
        80063ac8 21 38 20 02     _move      a3,s1
        80063acc fe fd 03 0c     jal        0x800ff7f8 // Set Digimon Adquired Trigger
        80063ad0 ff ff 04 32     _andi      a0,s0,0xffff
        80063ad4 13 80 02 3c     lui        v0,0x8013
        80063ad8 d1 ce 42 24     addiu      v0,v0,-0x312f
        80063adc 21 10 52 00     addu       v0,v0,s2
        80063ae0 00 00 43 90     lbu        v1,0x0(v0)
        80063ae4 4c 00 a2 8f     lw         v0,0x4c(sp)
        80063ae8 00 00 00 00     nop
        80063aec 02 00 43 10     beq        v0,v1,0x80063af8 // LAB_80063af8
        80063af0 00 00 00 00     _nop
        80063af4 56 00 80 a6     sh         zero,0x56(s4)
                             LAB_80063af8                                   
        80063af8 44 00 bf 8f     lw         ra,0x44(sp)
        80063afc 40 00 be 8f     lw         s8,0x40(sp)
        80063b00 3c 00 b7 8f     lw         s7,0x3c(sp)
        80063b04 38 00 b6 8f     lw         s6,0x38(sp)
        80063b08 34 00 b5 8f     lw         s5,0x34(sp)
        80063b0c 30 00 b4 8f     lw         s4,0x30(sp)
        80063b10 2c 00 b3 8f     lw         s3,0x2c(sp)
        80063b14 28 00 b2 8f     lw         s2,0x28(sp)
        80063b18 24 00 b1 8f     lw         s1,0x24(sp)
        80063b1c 20 00 b0 8f     lw         s0,0x20(sp)
        80063b20 08 00 e0 03     jr         ra
        80063b24 60 00 bd 27     _addiu     sp,sp,0x60
