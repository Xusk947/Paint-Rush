Shader "Custom/FillShader" {
    Properties {
        _MainTex ("Texture", 2D) = "white" {}
        _FillColor ("Fill Color", Color) = (1, 0, 0, 1)
        _FillPercentage ("Fill Percentage", Range(0, 1)) = 0.5
    }

    SubShader {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Lambert

        sampler2D _MainTex;
        fixed4 _FillColor;
        float _FillPercentage;

        struct Input {
            float2 uv_MainTex;
        };

        void surf (Input IN, inout SurfaceOutput o) {
            fixed4 color = tex2D(_MainTex, IN.uv_MainTex);

            if (IN.uv_MainTex.x <= _FillPercentage)
                o.Albedo = _FillColor;
            else
                o.Albedo = color;
        }
        ENDCG
    }

    FallBack "Diffuse"
}