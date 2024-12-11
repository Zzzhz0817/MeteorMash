Shader "Custom/InsideObjectShader"
{
    Properties
    {
        _MainTex ("Base (RGB)", 2D) = "white" { }
    }

    SubShader
    {
        Tags { "Queue" = "Overlay" }

        Pass
        {
            // Set up the stencil buffer to only render inside the container
            Stencil
            {
                Ref 1          // Match the reference value 1 from the container
                Comp equal     // Only pass if stencil value is equal to 1
                Pass keep      // Don't modify the stencil buffer
            }

            // Set transparency for the inside object
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite On
            ColorMask RGB
            Lighting Off

            // Render the inside object
            SetTexture[_MainTex] { combine primary }
        }
    }
    
    FallBack "Diffuse"
}
