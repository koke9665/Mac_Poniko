using UnityEngine;

namespace Ph.Effects
{
    [ExecuteInEditMode]
#if UNITY_5_4_OR_NEWER
    [ImageEffectAllowedInSceneView]
#endif
    public class ScaleWipe : MonoBehaviour
    {
        [SerializeField]
        Shader m_shader;

        Material m_material;
        public Material material
        {
            get
            {
                // 遅延初期化
                if (m_material == null)
                {
                    m_material = new Material(m_shader);                // ShaderからMaterialを作成
                    m_material.hideFlags = HideFlags.HideAndDontSave;   // シーンに保存されないように指定
                }
                return m_material;
            }
        }

        [Range(0f, 1f)]
        public float radius = 1.0f;

        void LateUpdate()
        {
            material.SetFloat("_Radius", radius);
        }

        void OnRenderImage(RenderTexture source, RenderTexture destination)
        {
            // materialを用いて画面を描画
            // source(RenderTexture)が_MainTex(Shader側)にセットされる
            // 描画先はdestination(RenderTexture)
            Graphics.Blit(source, destination, material);
        }

        void OnDisable()
        {
            if (m_material != null) DestroyImmediate(m_material);       // スクリプト無効時にMaterialを削除
        }
    }
}