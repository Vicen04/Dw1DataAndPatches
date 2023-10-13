//This patch fixes the oversight the developers made with the evolutions
//When the code treis to calculate an evolution, it does not reset all the data, unless the evolution is possible. This can cause the last evolutions to be calculated incorrectly
//This patch is not compatible with either my hack or Maeson's hack

//If you are interested in the full original code, SydMontague has it on Github

//This example is applied to rookie and champion targets
Original:

GetEvolutionTarget(int digimonType)
{
// code ignored
if (requirementScore > 2)
 {
//code ignored

  if(bestScore < score)
  {
      bestScore = score;
      bestDigimon = targetType;
      
      count = 0;
      score = 0;
  }
 }
// code ignored
}

Changed:

GetEvolutionTarget(int digimonType)
{
if (requirementScore > 2)
 {
//code ignored

  if(bestScore < score) 
  {
      bestScore = score;
      bestDigimon = targetType;
  }
 }
      count = 0;
      score = 0;
// code ignored
}

Disassembly:

        Offset       Hex         Commands

//Rookie

Original:

        800e2c88 84 00 20 14     bne        at,zero,LAB_800e2e9c


        800e2e7c 07 00 20 10     beq        at,zero,0x800e2e9c
        800e2e80 00 00 00 00     _nop
        800e2e84 00 9c 10 00     sll        s3,s0,0x10
        800e2e88 00 84 00 00     sll        s0,zero,0x10
        800e2e8c 21 a0 03 00     move       s4,v1
        800e2e90 03 9c 13 00     sra        s3,s3,0x10
        800e2e94 21 88 00 00     clear      s1

Changed:
        800e2c88 81 00 20 14     bne        at,zero,0x800e2e90


        800e2e7c 04 00 20 10     beq        at,zero,0x800e2e90
        800e2e80 00 00 00 00     _nop
        800e2e84 00 9c 10 00     sll        s3,s0,0x10
        800e2e88 21 a0 03 00     move       s4,v1                              
        800e2e8c 03 9c 13 00     sra        s3,s3,0x10
                             LAB_800e2e90  
        800e2e90 00 84 00 00     sll        s0,zero,0x10
        800e2e94 21 88 00 00     clear      s1


//Champion

Original:

        800e2fdc 84 00 20 14     bne        at,zero,0x800e31f0


        800e31d0 07 00 20 10     beq        at,zero,0x800e31f0
        800e31d4 00 00 00 00     _nop
        800e31d8 00 9c 10 00     sll        s3,s0,0x10
        800e31dc 00 84 00 00     sll        s0,zero,0x10
        800e31e0 21 a8 60 00     move       s5,v1
        800e31e4 03 9c 13 00     sra        s3,s3,0x10
        800e31e8 21 88 00 00     clear      s1

Changed:

        800e2fdc 81 00 20 14     bne        at,zero,0x800e31e4


        800e31d0 04 00 20 10     beq        at,zero,0x800e31e4
        800e31d4 00 00 00 00     _nop
        800e31d8 00 9c 10 00     sll        s3,s0,0x10
        800e31dc 21 a8 60 00     move       s5,v1                              
        800e31e0 03 9c 13 00     sra        s3,s3,0x10
                             LAB_800e31e4  
        800e31e4 00 84 00 00     sll        s0,zero,0x10
        800e31e8 21 88 00 00     clear      s1
