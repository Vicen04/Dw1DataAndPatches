//This patch show the changes done to avoid two possibles softlocks

WaitForEntityToMoveTo(scriptId1, scriptId2, targetPosX, targetPosZ, duration, val6)
{

//code ignored
    if (speedX < 0) 
    {
      if (entityPosX <= targetPosX)
      {
        entityPosX = targetPosX;
        bVar1 = true;
      }
    }
    else if ((speedX == 0) || (targetPosX <= entityPosX)) //makes sure it stops and sets the data if the speed is 0 (this happens if its too close to the target)
    {
      entityPosX = targetPosX;
      bVar1 = true;
    }

    if (speedZ < 0)
   {
      if (entityPosZ <= targetPosZ) 
      {
        entityPosX = targetPosZ;
        bVar2 = true;
      }
    }
    else if ((speedZ == 0) || (targetPosZ <= entityPosZ)) //makes sure it stops and sets the data if the speed is 0 (this happens if its too close to the target)
    {
      entityPosX = targetPosZ;
      bVar2 = true;
    }
//code ignored
}

Disassembly:

                    WaitForEntityToMoveTo    

         Offset       Hex         Commands

        800ac714 04 00 05 8e     lw         a0,0x4(s0) 
        800ac718 03 1e 03 00     sra        v1,v1,0x18
        800ac71c 09 00 41 04     bgez       v0,0x800ac744
        800ac720 00 00 00 00     _nop
        800ac724 78 00 a2 8c     lw         v0,0x78(a1) //entityPosX
        800ac728 00 00 00 00     nop
        800ac72c 2a 08 22 02     slt        at,s1,v0
        800ac730 0d 00 20 14     bne        at,zero,0x800ac768
        800ac734 00 00 00 00     _nop
        800ac738 78 00 b1 ac     sw         s1,0x78(a1) //entityPosX
        800ac73c 0a 00 00 10     b          0x800ac768
        800ac740 01 00 03 24     _li        v1,0x1
                             LAB_800ac744                                   
        800ac744 06 00 02 10     beq        zero,v0,0x800ac760
        800ac748 00 00 00 00     _nop
        800ac74c 78 00 a2 8c     lw         v0,0x78(a1) //entityPosX
        800ac750 00 00 00 00     nop
        800ac754 2a 08 51 00     slt        at,v0,s1
        800ac758 03 00 20 14     bne        at,zero,0x800ac768
        800ac75c 00 00 00 00     _nop
                             LAB_800ac760                                   
        800ac760 78 00 b1 ac     sw         s1,0x78(a1) //entityPosX
        800ac764 01 00 03 24     li         v1,0x1
                             LAB_800ac768                                   
        800ac768 6e 91 82 87     lh         v0,-0x6e92(gp) //speedZ
        800ac76c 00 00 00 00     nop
        800ac770 04 00 05 8e     lw         a1,0x4(s0) 
        800ac774 00 00 00 00     nop
        800ac778 09 00 41 04     bgez       v0,0x800ac7a0
        800ac77c 00 00 00 00     _nop
        800ac780 80 00 a2 8c     lw         v0,0x80(a1) //entityPosZ
        800ac784 00 00 00 00     nop
        800ac788 2a 08 62 02     slt        at,s3,v0
        800ac78c 0d 00 20 14     bne        at,zero,0x800ac7c4
        800ac790 00 00 00 00     _nop
        800ac794 80 00 b3 ac     sw         s3,0x80(a1) //entityPosZ
        800ac798 0a 00 00 10     b          0x800ac7c4
        800ac79c 01 00 04 24     _li        a0,0x1
                             LAB_800ac7a0                                    
        800ac7a0 06 00 02 10     beq        zero,v0,0x800ac7bc
        800ac7a4 00 00 00 00     _nop
        800ac7a8 80 00 a2 8c     lw         v0,0x80(a1) //entityPosZ
        800ac7ac 00 00 00 00     nop
        800ac7b0 2a 08 53 00     slt        at,v0,s3
        800ac7b4 03 00 20 14     bne        at,zero,0x800ac7c4
        800ac7b8 00 00 00 00     _nop
                             LAB_800ac7bc                                  
        800ac7bc 80 00 b3 ac     sw         s3,0x80(a1) //entityPosZ
        800ac7c0 01 00 04 24     li         a0,0x1



WaitForEntityToMoveToAxis(scriptId, targetValue, axis, duration, withCamera)
{
//code ignored
    if (speed == 0) //makes sure it stops and sets the data if the speed is 0 (this happens if its too close to the target)
    {
      entityAxisChoosen = targetValue;
      DAT_80134c80 = 0;
      return 1;
    }
    if (speed < 0) 
    {
      if (entityAxis <= targetValue) 
      {
        entityAxisChoosen = targetValue;
        DAT_80134c80 = 0;
        return 1;
      }
    }
    else if (targetValue <= entityAxis) 
    {
      entityAxisChoosen = targetValue;
      cDAT_80134c80 = 0;
      return 1;
    }
//code ignored
}

Disassembly:

                    WaitForEntityToMoveToAxis    

         Offset       Hex         Commands

        800ac97c 08 00 02 10     beq        zero,v0,0x800ac9a0
        800ac980 00 00 00 00     _nop
        800ac984 00 00 01 8e     lw         at,0x0(s0) //entityAxis
        800ac988 00 00 00 00     nop
        800ac98c 08 00 41 04     bgez       v0,0x800ac9b0
        800ac990 00 00 00 00     _nop
        800ac994 2a 08 41 02     slt        at,s2,at
        800ac998 0c 00 20 14     bne        at,zero,0x800ac9cc
        800ac99c 00 00 00 00     _nop
                             LAB_800ac9a0                                    
        800ac9a0 00 00 12 ae     sw         s2,0x0(s0) entityAxis
        800ac9a4 54 91 80 a3     sb         zero,-0x6eac(gp) //DAT_80134c80
        800ac9a8 1e 00 00 10     b          0x800aca24
        800ac9ac 01 00 02 24     _li        v0,0x1
                             LAB_800ac9b0                                   
        800ac9b0 2a 08 32 00     slt        at,at,s2
        800ac9b4 05 00 20 14     bne        at,zero,0x800ac9cc
        800ac9b8 00 00 00 00     _nop
        800ac9bc 00 00 12 ae     sw         s2,0x0(s0) entityAxis
        800ac9c0 54 91 80 a3     sb         zero,-0x6eac(gp) // DAT_80134c80
        800ac9c4 17 00 00 10     b          0x800aca24
        800ac9c8 01 00 02 24     _li        v0,0x1

