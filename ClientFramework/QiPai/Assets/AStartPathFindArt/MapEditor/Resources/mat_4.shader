

Shader "Custom/MapMesh" { 
    Properties 
	{ 

        _Color("Main color",Color) = (1,1,1,0) 
    } 
    SubShader { 
        Pass 
        { 
            Material 
            { 
                Diffuse[_Color] 
            } 
        } 
    } 
	FallBack "Diffuse"
} 
