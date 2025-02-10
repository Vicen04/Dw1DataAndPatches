//These changes are part of hardcore 1.1


//Infinte MP

Original: 
CheckMPandCreateTech(int *AttackingEntity, int TargetEntity, int Offset)
{

//code ignored
   if (AttackingEntity == PartnerPtr)
      {
    if ((TechValue < 58) || (112 < TechValue)) 
      {
      TechCost = TechMPCost[TechValue] * 3
      if (Injured || Sick)  
       {
        TechExtraCost = (TechMPCost[TechValue] * 3) /2;
        if (TechExtraCost <= 0) 
          TechExtraCost = 1;
        
        TechCost = TechCost + TechExtraCost;
      }
      BrainsStat = AttackingEntityBrains;
      if (699 < BrainsStat)
      {
        if (BrainsStat == 999) 
          TechCost = (TechCost * 4) / 5; //20% reduction
        
        else if ((BrainsStat < 900) || (998 < BrainsStat))
        {
          if ((BrainsStat < 800) || (899 < BrainsStat)) 
            TechCost = (TechCost * 19) / 20; //5% reduction
          
          else 
            TechCost = (TechCost * 9) / 10); //10% reduction
          
        }
        else {
          TechCost = (TechCost * 17) / 20; //15% reduction
        }
      }
      PartnerMPUsed = PartnerMPUsed + TechCost; //This is reset each time the screen updates
    }
  }
  else if (((TechValue < 58) || (112 < TechValue)) && (WillRunFromBattle == 0)) //Neither a finisher or a boss battle
 {
    AttackingEntityMP =  AttackingEntityMP - TechMPCost[TechValue] * 3;
    
 }

//code ignored
}



Changed:

CheckMPandCreateTech(int *AttackingEntity, int TargetEntity, int Offset)
{

//code ignored
   if (AttackingEntity == PartnerPtr)
      {
    if ((TechValue < 58) || (112 < TechValue)) 
      {
      TechCost = TechMPCost[TechValue] * 3
      if (Injured || Sick)  
       {
        TechExtraCost = (TechMPCost[TechValue] * 3) /2;
        if (TechExtraCost <= 0) 
          TechExtraCost = 1;
        
        TechCost = TechCost + TechExtraCost;
      }
      BrainsStat = AttackingEntityBrains;
      if (699 < BrainsStat)
      {
        if (BrainsStat == 999) 
          TechCost = (TechCost * 4) / 5; //20% reduction
        
        else if ((BrainsStat < 900) || (998 < BrainsStat))
        {
          if ((BrainsStat < 800) || (899 < BrainsStat)) 
            TechCost = (TechCost * 19) / 20; //5% reduction
          
          else 
            TechCost = (TechCost * 9) / 10); //10% reduction
          
        }
        else {
          TechCost = (TechCost * 17) / 20; //15% reduction
        }
      }
      PartnerMPUsed = PartnerMPUsed + TechCost; //This is reset each time the screen updates
    }
  }

 //The MP cost for the NPC is never triggered

//code ignored
}


Disassembly:


Original:
        8005db18 67 00 42 16     bne        s2,v0,0x8005dcb8

Changed:
        8005db18 7E 00 42 16     bne        s2,v0,0x8005dcb8




//Finishers buff


//Hardcore
int * CalculateMovementDamage(int AttackerPtr,int *DefenderPtr,int MoveID)

{
//code ignored
    if (AttackerPtr == PartnerPtr) // Your digimon
    {
      if (40 < FinisherCharge) //The Finisher charge goes up to 80
        FinalDamage = Damage * FinisherCharge / 40); //Only applies if you filled half or more
      
    }
    else
    {
      ExtraBoost = ReturnRandom(201);
      FinalDamage = (Damage * (ExtraBoost + 200)) / 100);
    }

//code ignored
}

//True Hardcore
int * CalculateMovementDamage(int AttackerPtr,int *DefenderPtr,int MoveID)

{
//code ignored
    if (AttackerPtr == PartnerPtr) // Your digimon
    {
      if (40 < FinisherCharge) //The Finisher charge goes up to 80
        FinalDamage = Damage * FinisherCharge / 40); //Only applies if you filled half or more
      
    }
    else
    {
      ExtraBoost = ReturnRandom(201);
      FinalDamage = (Damage * (ExtraBoost + 300)) / 100);
    }

//code ignored
}


Disassembly:


Original:
        8005c178 65 00 04 24     _li        a0,0x65
        8005c17c 64 00 42 20     addi       v0,v0,0x64

Hardcore:
        8005c178 c9 00 04 24     _li        a0,0xc9
        8005c17c c8 00 42 20     addi       v0,v0,0xc8

True Hardcore:
        8005c178 c9 00 04 24     _li        a0,0xc9
        8005c17c 2c 01 42 20     addi       v0,v0,0x12c
