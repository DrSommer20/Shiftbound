using System;
using System.Collections.Generic;
using System.Text;

namespace Assets.Scripts.Enemies
{
    using UnityEngine;

    public class MushroomAI : MonoBehaviour
    {
        private EnemyBase enemy;
        private bool isWaitng;

        private void Start()
        {
            enemy = GetComponent<EnemyBase>();
        }

        private void Update()
        {
            // 1. Überprüfe die Umgebung
            bool wallDetected = enemy.IsDetectingWall();
            bool groundAhead = enemy.IsDetectingGround();

            // 2. Logik: Wenn Wand voraus ODER kein Boden mehr voraus -> Umdrehen
            if (wallDetected || !groundAhead)
            {
                enemy.Flip();
            }
        }

        private void FixedUpdate()
        {
            // 3. Bewegung ausführen
            // Wir nutzen die FacingDirection aus der EnemyBase (1 für Rechts, -1 für Links)
            enemy.RB.linearVelocity = new Vector2(enemy.moveSpeed * enemy.FacingDirection, enemy.RB.linearVelocity.y);
        }
    }
}
