Shader "Custom/UnlitTransparent"
{
    Properties
    {
        _MainTex ("Main Texture", 2D) = "white" {} // Texture
        _Color ("Tint Color", Color) = (1,1,1,1)  // Tint color
    }
    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        LOD 100

        Pass
        {
            // Enable alpha blending
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            Cull Off

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            // Vertex input structure
            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            // Vertex-to-fragment structure
            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;  // The main texture
            float4 _Color;       // Tint color

            v2f vert (appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex); // Transform vertex position
                o.uv = v.uv;                              // Pass UV coordinates
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // Sample the texture color
                fixed4 texColor = tex2D(_MainTex, i.uv);

                // Apply the tint color (multiplying RGB, keeping alpha)
                texColor.rgb *= _Color.rgb;   // Tint the texture color
                texColor.a *= _Color.a;      // Keep alpha blending intact

                return texColor;
            }
            ENDCG
        }
    }
}
