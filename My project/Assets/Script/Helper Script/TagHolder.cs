using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Axis
{
    public const string HORIZONTAL = "Horizontal";
    public const string VERTICAL = "Vertical";
}

public class MouseAxis
{
    public const string MOUSE_X = "Mouse X";
    public const string MOUSE_Y = "Mouse Y";
}

public class AnimationTags
{

    public const string ZOOM_IN_ANIM = "ZoomIn";
    public const string ZOOM_OUT_ANIM = "ZoomOut";

    public const string WEAPON_IDLE_ANIM = "Idle";
    public const string WEAPON_ATTACK_ANIM = "Attack";

    public const string SHOOT_TRIGGER = "Shoot";
    public const string AIM_PARAMETER = "Aim";

    public const string WALK_PARAMETER = "Walk";
    public const string RUN_PARAMETER = "Run";
    public const string ATTACK_TRIGGER = "Attack";
    public const string DEAD_TRIGGER = "Dead";

}

public class Tags
{
    // helper
    public const string LOOK_ROOT = "Look Root";
    public const string ZOOM_CAMERA = "FP Camera";
    public const string CROSSHAIR = "Crosshair";

    // Weapon
    public const string AXE_TAG = "Axe";
    // Bullet
    public const string ARROW_TAG = "Arrow";
    public const string SPEAR_TAG = "Spear";
   
    // Role
    public const string PLAYER_TAG = "Player";
    public const string ENEMY_TAG = "Enemy";
    // Detailed type of enemy
    public const string CANNIBAL = "Cannibal";
    public const string BOAR = "Boar";
}

