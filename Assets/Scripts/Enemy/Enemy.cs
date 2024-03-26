using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;


namespace Game
{
    public class Enemy : Character, IPoolObject
    {
        public EnemyMovement enemyMovement;
        public EnemyAttackManager enemyAttackManager;

        private Collider _collider;

        [SerializeField] private SkinnedMeshRenderer _skinnedMeshRenderer;
        private Material objectMaterial;

        [SerializeField] private SpriteRenderer shadowDot;

        private void Start()
        {
            objectMaterial = new Material(_skinnedMeshRenderer.sharedMaterial);
            _skinnedMeshRenderer.sharedMaterial = objectMaterial;
            _collider = GetComponent<Collider>();
        }
        public override void OnDied()
        {
            if(TargetManager.Instance.targets.Contains(this.transform))
                TargetManager.Instance.targets.Remove(this.transform);

            GameManager.Instance.UpdateKill();

            StartCoroutine(Dissolve());

            _collider.enabled = false;
            enemyAttackManager.StopAllCoroutines();
            enemyAttackManager.enabled = false;
            enemyMovement.enabled = false;
            base.OnDied();
            Invoke(nameof(ReturnToPool), 3);
        }

        public void ReturnToPool()
        {
            gameObject.SetActive(false);
            CurrentCharacterState = CharacterState.Running;
            enemyMovement.enabled = true;
            enemyMovement.CanMove = true;
            enemyAttackManager.enabled = true;

            objectMaterial.SetFloat("_Dissolve_Value", 0);

            _collider.enabled = true;
        }
        private IEnumerator Dissolve()
        {
            float t = 0;

            while (t < 1)
            {
                yield return null;
                t += Time.deltaTime / 2;

                Color color = shadowDot.color;
                color.a = Mathf.Lerp(color.a, 0, t);
                shadowDot.color = color;


                objectMaterial.SetFloat("_Dissolve_Value", Mathf.Lerp(0, 0.8f, t));
            }
            objectMaterial.SetFloat("_Dissolve_Value", .8f);
        }
    }
}

