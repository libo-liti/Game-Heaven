using UnityEngine;

public class Bullet_PlayerAnimation : MonoBehaviour
{
    public Animator Player_Animator;
    public SpriteRenderer Player;
    Vector2 PlayerVec;


    void Awake()
    {
        Player_Animator = GetComponent<Animator>();
    }

    internal void Player_Move_Animation(float Player_Move_x, float Player_Move_y)
    {
        PlayerVec = new Vector2(Player_Move_x, Player_Move_y);

        Player_Animator.SetFloat("MoveVec.y", PlayerVec.y);
        if (PlayerVec.x != 0)
        {
            Player_Animator.SetBool("Move_Side", true);
            if (PlayerVec.x < 0)
            {
                Player.flipX = false;
            }
            else if (PlayerVec.x > 0)
            {
                Player.flipX = true;
            }
        }
        else
        {
            Player_Animator.SetBool("Move_Side", false);
        }

        if (PlayerVec.x == 0 && PlayerVec.y == 0)
        {
            Player_Animator.SetBool("IsMoving", false);
        }
        else
        {
            Player_Animator.SetBool("IsMoving", true);
        }
    }

    internal void Player_Hit_True_Animation()
    {
        Player_Animator.SetBool("Player_Hit",true);
    }

    internal void Player_Hit_False_Animation()
    {
        Player_Animator.SetBool("Player_Hit",false);
    }

    internal void Player_Die_Animation()
    {
        Player_Animator.SetTrigger("Player_Die");
    }

    internal void Player_Idle_Animation()
    {
        Player_Animator.SetTrigger("Player_Idle");
    }
}
