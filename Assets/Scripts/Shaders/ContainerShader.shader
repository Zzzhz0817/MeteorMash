Shader "Custom/ContainerShader"
{
    Properties
    {
        _MainTex ("Base (RGB)", 2D) = "white" { }
    }
    
    SubShader
    {
        Tags { "Queue" = "Background" }

        Pass
        {
            // Set up the stencil buffer to mark the inside region
            Stencil
            {
                Ref 1          // Write reference value 1 to the stencil buffer
                Comp always    // Always pass the stencil test
                Pass replace   // Replace stencil value with 1
            }

            // Set the transparency and color for the container (semi-transparent sphere)
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite On
            ColorMask RGB
            Lighting Off

            SetTexture[_MainTex] { combine primary }
        }
    }
    
    FallBack "Diffuse"
}