﻿using System.Collections.Generic;
using UnityEngine;

public interface ICharacter
{
    CharacterStats Stats { get; set; }
    Health Health { get; set; }
    List<ICharacter> CurrentEnnemies { get; set; }
    List<ICharacter> TeamMembers { get; set; }
    int MovementPoints { get; set; }
    PlayerDirection Direction { get; set; }
    CharacterType CharacterType { get; set; }

    //Cell CurrentCoordinates { get; set; }
    //Cell OldCoordinates { get; set; }
    int[] Position { get; set; }

    Animator Animator { get; set; }
}