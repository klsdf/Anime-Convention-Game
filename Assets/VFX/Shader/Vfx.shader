// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Custom/Vfx"
{
	Properties
	{
		[HideInInspector] _EmissionColor("Emission Color", Color) = (1,1,1,1)
		[HideInInspector] _AlphaCutoff("Alpha Cutoff ", Range(0, 1)) = 0.5
		[ASEBegin][Header(01                                                            BasicSettings)][Enum(UnityEngine.Rendering.BlendMode)]Src("Src", Float) = 5
		[Enum(UnityEngine.Rendering.BlendMode)]Dst("Dst", Float) = 10
		[Enum(UnityEngine.Rendering.CullMode)]Cullmode("剔除模式", Float) = 0
		[Enum(Off,0,On,1)]shendu("深度", Float) = 0
		[Enum(UnityEngine.Rendering.CompareFunction)]shenduceshi("深度测试", Float) = 4
		[Toggle(KAIQIRUANLIZI_ON)] kaiqiruanlizi("开启软粒子", Float) = 0
		DepthFade("软粒子", Range( 0 , 10)) = 0
		zhengtitoumingdu("整体透明度", Range( 0 , 1)) = 1
		[HDR][Header(02                                                            MainTexture)]zhuwenli_color("主纹理_颜色", Color) = (1,1,1,1)
		zhuwenli("主纹理", 2D) = "white" {}
		[Toggle]shiyongRtongdao("使用R通道", Float) = 0
		[Toggle(JIZUOBIAO_ON)] jizuobiao("极坐标", Float) = 0
		zhuwenlijizuobiaoTiling_Offset("主纹理极坐标Tiling_Offset", Vector) = (1,1,0,0)
		[Toggle]zhuwenliUClamp("主纹理U_Clamp", Float) = 0
		[Toggle]zhuwenliVClamp1("主纹理V_Clamp", Float) = 0
		zhuwenliUliudong("主纹理U流动", Float) = 0
		zhuwenliVliudong("主纹理V流动", Float) = 0
		[HDR][Header(03                                                            AddTexture)]fujiatietuyanse("附加贴图颜色", Color) = (0,0,0,1)
		fujiatietu("附加贴图", 2D) = "white" {}
		[Toggle(KAIQIFUJIATIETU_ON)] kaiqifujiatietu("开启附加贴图", Float) = 0
		[KeywordEnum(Add,Multiply)] fujiatietudiejiamoshi("附加贴图叠加模式", Float) = 0
		fujiatietujizuobiaoTiling_Offset1("附加贴图极坐标Tiling_Offset", Vector) = (1,1,0,0)
		fujiatietuUliudong("附加贴图U流动", Float) = 0
		fujiatietuVliudong("附加贴图V流动", Float) = 0
		[Header(04                                                            MaskTexture)]zhezhaoTex("遮罩", 2D) = "white" {}
		[Toggle(KAIQIZHEZHAO_ON)] kaiqizhezhao("开启遮罩", Float) = 0
		zhezhaojizuobiaoTiling_Offset1("遮罩极坐标Tiling_Offset", Vector) = (1,1,0,0)
		zhezhaoUliudong("遮罩U流动", Float) = 0
		zhezhaoVliudong("遮罩V流动", Float) = 0
		zhezhaoqiangdu("遮罩强度", Range( -1 , 1)) = 0
		zhezhaoTex2("遮罩2", 2D) = "white" {}
		zhezhaoUliudong1("遮罩2U流动", Float) = 0
		zhezhaoVliudong1("遮罩2V流动", Float) = 0
		[Header(05                                                            UVDistortTexture)]UVniuquTex("UV扭曲", 2D) = "white" {}
		[Toggle(KAIQINIUQU_ON)] kaiqiniuqu("开启扭曲", Float) = 0
		[Toggle]kaiqiniuqufangxiang("开启扭曲反向", Float) = 0
		UVniuqujizuobiaoTiling_Offset("UV扭曲极坐标Tiling_Offset", Vector) = (1,1,0,0)
		UVniuquUliudong("UV扭曲U流动", Float) = 0
		UVniuquVliudong("UV扭曲V流动", Float) = 0
		UVniuquqiangdu("UV扭曲强度", Float) = 0
		zhuwenliniuqu("主纹理扭曲", Range( 0 , 1)) = 0
		fujiatietuniuqu("附加贴图扭曲", Range( 0 , 1)) = 0
		zhezhaoniuqu("遮罩扭曲", Range( 0 , 1)) = 0
		rongjieniuqu("溶解扭曲", Range( 0 , 1)) = 0
		dingdianpianyiniuqu("顶点偏移扭曲", Range( 0 , 1)) = 0
		[Header(06                                                            DissolveTexture)]rongjietietu("溶解贴图", 2D) = "white" {}
		[Toggle(KAIQIRONGJIE_ON)] kaiqirongjie("开启溶解", Float) = 0
		[Toggle]kaiqirongjiefanx("开启溶解反向", Float) = 0
		rongjiejizuobiaoTiling_Offset("溶解极坐标Tiling_Offset", Vector) = (1,1,0,0)
		rongjiejindu("溶解进度", Range( 0 , 1)) = 0
		rongjieruanyingdu("溶解软硬度", Range( 0 , 0.5)) = 0
		rongjiemiaobianfanwei("溶解描边范围", Range( 0 , 1)) = 0
		rongjiemiaobiankuangdu("溶解描边宽度", Range( 0 , 0.5)) = 0.5
		[HDR]rongjieliangbianyanse("溶解亮边颜色", Color) = (1,1,1,1)
		rongjieUliudong("溶解U流动", Float) = 0
		rongjieVliudong("溶解V流动", Float) = 0
		[Header(07                                                            VertexOffsetTexture)]dindianpianyi("顶点偏移", 2D) = "white" {}
		[Toggle(KAIQIDINGDIANPIANYI_ON)] kaiqidingdianpianyi("开启顶点偏移", Float) = 0
		dindianpianyiqiangdu("顶点偏移强度", Float) = 0
		dindianpianyigaodu("顶点外偏移高度", Float) = 0
		dindianneiwaiqiangdu("顶点内外强度", Float) = 1
		dindianpianyiUliudong("顶点偏移U流动", Float) = 0
		dindianpianyiVliudong("顶点偏移V流动", Float) = 0
		dindianpianyizhezhao("顶点偏移遮罩", 2D) = "white" {}
		dindianpianyizhezhaoUliudong("顶点偏移遮罩U流动", Float) = 0
		dindianpianyizhezhaoVliudong("顶点偏移遮罩V流动", Float) = 0
		[HDR][Header(08                                                            Fresnel)]feinieryanse("菲尼尔颜色", Color) = (1,1,1,1)
		[Toggle(KAIQIFEINIER_ON)] kaiqifeinier("开启菲尼尔", Float) = 0
		[Toggle]fanxiangfeinier("反向菲尼尔", Float) = 0
		[KeywordEnum(Add,Multiply)] feiniermoshi("菲尼尔模式", Float) = 0
		feinierruihua("菲尼尔锐化", Float) = 1
		[ASEEnd]feinierqiangdu("菲尼尔强度", Float) = 1

		//_TessPhongStrength( "Tess Phong Strength", Range( 0, 1 ) ) = 0.5
		//_TessValue( "Tess Max Tessellation", Range( 1, 32 ) ) = 16
		//_TessMin( "Tess Min Distance", Float ) = 10
		//_TessMax( "Tess Max Distance", Float ) = 25
		//_TessEdgeLength ( "Tess Edge length", Range( 2, 50 ) ) = 16
		//_TessMaxDisp( "Tess Max Displacement", Float ) = 25
	}

	SubShader
	{
		LOD 0

		
		Tags { "RenderPipeline"="UniversalPipeline" "RenderType"="Transparent" "Queue"="Transparent" }
		
		Cull [Cullmode]
		AlphaToMask Off
		HLSLINCLUDE
		#pragma target 2.0

		#ifndef ASE_TESS_FUNCS
		#define ASE_TESS_FUNCS
		float4 FixedTess( float tessValue )
		{
			return tessValue;
		}
		
		float CalcDistanceTessFactor (float4 vertex, float minDist, float maxDist, float tess, float4x4 o2w, float3 cameraPos )
		{
			float3 wpos = mul(o2w,vertex).xyz;
			float dist = distance (wpos, cameraPos);
			float f = clamp(1.0 - (dist - minDist) / (maxDist - minDist), 0.01, 1.0) * tess;
			return f;
		}

		float4 CalcTriEdgeTessFactors (float3 triVertexFactors)
		{
			float4 tess;
			tess.x = 0.5 * (triVertexFactors.y + triVertexFactors.z);
			tess.y = 0.5 * (triVertexFactors.x + triVertexFactors.z);
			tess.z = 0.5 * (triVertexFactors.x + triVertexFactors.y);
			tess.w = (triVertexFactors.x + triVertexFactors.y + triVertexFactors.z) / 3.0f;
			return tess;
		}

		float CalcEdgeTessFactor (float3 wpos0, float3 wpos1, float edgeLen, float3 cameraPos, float4 scParams )
		{
			float dist = distance (0.5 * (wpos0+wpos1), cameraPos);
			float len = distance(wpos0, wpos1);
			float f = max(len * scParams.y / (edgeLen * dist), 1.0);
			return f;
		}

		float DistanceFromPlane (float3 pos, float4 plane)
		{
			float d = dot (float4(pos,1.0f), plane);
			return d;
		}

		bool WorldViewFrustumCull (float3 wpos0, float3 wpos1, float3 wpos2, float cullEps, float4 planes[6] )
		{
			float4 planeTest;
			planeTest.x = (( DistanceFromPlane(wpos0, planes[0]) > -cullEps) ? 1.0f : 0.0f ) +
						  (( DistanceFromPlane(wpos1, planes[0]) > -cullEps) ? 1.0f : 0.0f ) +
						  (( DistanceFromPlane(wpos2, planes[0]) > -cullEps) ? 1.0f : 0.0f );
			planeTest.y = (( DistanceFromPlane(wpos0, planes[1]) > -cullEps) ? 1.0f : 0.0f ) +
						  (( DistanceFromPlane(wpos1, planes[1]) > -cullEps) ? 1.0f : 0.0f ) +
						  (( DistanceFromPlane(wpos2, planes[1]) > -cullEps) ? 1.0f : 0.0f );
			planeTest.z = (( DistanceFromPlane(wpos0, planes[2]) > -cullEps) ? 1.0f : 0.0f ) +
						  (( DistanceFromPlane(wpos1, planes[2]) > -cullEps) ? 1.0f : 0.0f ) +
						  (( DistanceFromPlane(wpos2, planes[2]) > -cullEps) ? 1.0f : 0.0f );
			planeTest.w = (( DistanceFromPlane(wpos0, planes[3]) > -cullEps) ? 1.0f : 0.0f ) +
						  (( DistanceFromPlane(wpos1, planes[3]) > -cullEps) ? 1.0f : 0.0f ) +
						  (( DistanceFromPlane(wpos2, planes[3]) > -cullEps) ? 1.0f : 0.0f );
			return !all (planeTest);
		}

		float4 DistanceBasedTess( float4 v0, float4 v1, float4 v2, float tess, float minDist, float maxDist, float4x4 o2w, float3 cameraPos )
		{
			float3 f;
			f.x = CalcDistanceTessFactor (v0,minDist,maxDist,tess,o2w,cameraPos);
			f.y = CalcDistanceTessFactor (v1,minDist,maxDist,tess,o2w,cameraPos);
			f.z = CalcDistanceTessFactor (v2,minDist,maxDist,tess,o2w,cameraPos);

			return CalcTriEdgeTessFactors (f);
		}

		float4 EdgeLengthBasedTess( float4 v0, float4 v1, float4 v2, float edgeLength, float4x4 o2w, float3 cameraPos, float4 scParams )
		{
			float3 pos0 = mul(o2w,v0).xyz;
			float3 pos1 = mul(o2w,v1).xyz;
			float3 pos2 = mul(o2w,v2).xyz;
			float4 tess;
			tess.x = CalcEdgeTessFactor (pos1, pos2, edgeLength, cameraPos, scParams);
			tess.y = CalcEdgeTessFactor (pos2, pos0, edgeLength, cameraPos, scParams);
			tess.z = CalcEdgeTessFactor (pos0, pos1, edgeLength, cameraPos, scParams);
			tess.w = (tess.x + tess.y + tess.z) / 3.0f;
			return tess;
		}

		float4 EdgeLengthBasedTessCull( float4 v0, float4 v1, float4 v2, float edgeLength, float maxDisplacement, float4x4 o2w, float3 cameraPos, float4 scParams, float4 planes[6] )
		{
			float3 pos0 = mul(o2w,v0).xyz;
			float3 pos1 = mul(o2w,v1).xyz;
			float3 pos2 = mul(o2w,v2).xyz;
			float4 tess;

			if (WorldViewFrustumCull(pos0, pos1, pos2, maxDisplacement, planes))
			{
				tess = 0.0f;
			}
			else
			{
				tess.x = CalcEdgeTessFactor (pos1, pos2, edgeLength, cameraPos, scParams);
				tess.y = CalcEdgeTessFactor (pos2, pos0, edgeLength, cameraPos, scParams);
				tess.z = CalcEdgeTessFactor (pos0, pos1, edgeLength, cameraPos, scParams);
				tess.w = (tess.x + tess.y + tess.z) / 3.0f;
			}
			return tess;
		}
		#endif //ASE_TESS_FUNCS

		ENDHLSL

		
		Pass
		{
			
			Name "Forward"
			Tags { "LightMode" = "UniversalForward" }
			
			Blend [Src] [Dst]
			ZWrite [shendu]
			ZTest [shenduceshi]
			Offset 0 , 0
			ColorMask RGBA
			

			HLSLPROGRAM
			#define _RECEIVE_SHADOWS_OFF 1
			#pragma multi_compile_instancing
			#define ASE_SRP_VERSION 100202
			#define REQUIRE_DEPTH_TEXTURE 1

			#pragma prefer_hlslcc gles
			#pragma exclude_renderers d3d11_9x

			#pragma vertex vert
			#pragma fragment frag

			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/UnityInstancing.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderGraphFunctions.hlsl"

			#if ASE_SRP_VERSION <= 70108
			#define REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR
			#endif

			#define ASE_NEEDS_VERT_NORMAL
			#define ASE_NEEDS_FRAG_WORLD_POSITION
			#define ASE_NEEDS_FRAG_COLOR
			#pragma shader_feature_local KAIQIDINGDIANPIANYI_ON
			#pragma shader_feature_local KAIQINIUQU_ON
			#pragma shader_feature_local JIZUOBIAO_ON
			#pragma shader_feature_local KAIQIZHEZHAO_ON
			#pragma shader_feature_local KAIQIFEINIER_ON
			#pragma shader_feature_local FUJIATIETUDIEJIAMOSHI_ADD FUJIATIETUDIEJIAMOSHI_MULTIPLY
			#pragma shader_feature_local KAIQIFUJIATIETU_ON
			#pragma shader_feature_local KAIQIRONGJIE_ON
			#pragma shader_feature_local FEINIERMOSHI_ADD FEINIERMOSHI_MULTIPLY
			#pragma shader_feature_local KAIQIRUANLIZI_ON


			struct VertexInput
			{
				float4 vertex : POSITION;
				float3 ase_normal : NORMAL;
				float4 ase_texcoord : TEXCOORD0;
				float4 ase_texcoord1 : TEXCOORD1;
				float4 ase_texcoord2 : TEXCOORD2;
				float4 ase_color : COLOR;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct VertexOutput
			{
				float4 clipPos : SV_POSITION;
				#if defined(ASE_NEEDS_FRAG_WORLD_POSITION)
				float3 worldPos : TEXCOORD0;
				#endif
				#if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR) && defined(ASE_NEEDS_FRAG_SHADOWCOORDS)
				float4 shadowCoord : TEXCOORD1;
				#endif
				#ifdef ASE_FOG
				float fogFactor : TEXCOORD2;
				#endif
				float4 ase_color : COLOR;
				float4 ase_texcoord3 : TEXCOORD3;
				float4 ase_texcoord4 : TEXCOORD4;
				float4 ase_texcoord5 : TEXCOORD5;
				float4 ase_texcoord6 : TEXCOORD6;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};

			CBUFFER_START(UnityPerMaterial)
			float4 zhezhaoTex2_ST;
			float4 fujiatietuyanse;
			float4 zhuwenlijizuobiaoTiling_Offset;
			float4 zhuwenli_ST;
			float4 zhuwenli_color;
			float4 dindianpianyizhezhao_ST;
			float4 rongjietietu_ST;
			float4 rongjiejizuobiaoTiling_Offset;
			float4 fujiatietujizuobiaoTiling_Offset1;
			float4 rongjieliangbianyanse;
			float4 UVniuqujizuobiaoTiling_Offset;
			float4 UVniuquTex_ST;
			float4 fujiatietu_ST;
			float4 feinieryanse;
			float4 dindianpianyi_ST;
			float4 zhezhaojizuobiaoTiling_Offset1;
			float4 zhezhaoTex_ST;
			float feinierqiangdu;
			float fujiatietuniuqu;
			float zhezhaoniuqu;
			half rongjiemiaobianfanwei;
			half rongjieruanyingdu;
			float kaiqirongjiefanx;
			float rongjieUliudong;
			float rongjieVliudong;
			float feinierruihua;
			float zhezhaoUliudong;
			float zhengtitoumingdu;
			float shiyongRtongdao;
			float rongjieniuqu;
			float rongjiejindu;
			half rongjiemiaobiankuangdu;
			float fanxiangfeinier;
			float zhezhaoVliudong;
			float Src;
			float zhuwenliVClamp1;
			float fujiatietuUliudong;
			float Dst;
			float Cullmode;
			float shenduceshi;
			float shendu;
			float dindianneiwaiqiangdu;
			float dindianpianyiUliudong;
			float dindianpianyiVliudong;
			float kaiqiniuqufangxiang;
			float UVniuquUliudong;
			float UVniuquVliudong;
			float UVniuquqiangdu;
			float zhezhaoUliudong1;
			float zhezhaoVliudong1;
			float dingdianpianyiniuqu;
			float dindianpianyigaodu;
			float dindianpianyizhezhaoUliudong;
			float dindianpianyizhezhaoVliudong;
			float dindianpianyiqiangdu;
			float zhuwenliUClamp;
			float zhuwenliUliudong;
			float zhuwenliVliudong;
			float zhezhaoqiangdu;
			float zhuwenliniuqu;
			float fujiatietuVliudong;
			float DepthFade;
			#ifdef TESSELLATION_ON
				float _TessPhongStrength;
				float _TessValue;
				float _TessMin;
				float _TessMax;
				float _TessEdgeLength;
				float _TessMaxDisp;
			#endif
			CBUFFER_END
			sampler2D dindianpianyi;
			sampler2D UVniuquTex;
			sampler2D zhezhaoTex2;
			sampler2D dindianpianyizhezhao;
			sampler2D zhuwenli;
			sampler2D fujiatietu;
			sampler2D rongjietietu;
			sampler2D zhezhaoTex;
			uniform float4 _CameraDepthTexture_TexelSize;


						
			VertexOutput VertexFunction ( VertexInput v  )
			{
				VertexOutput o = (VertexOutput)0;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_TRANSFER_INSTANCE_ID(v, o);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);

				float2 appendResult8_g67 = (float2(dindianpianyiUliudong , dindianpianyiVliudong));
				float2 uvdindianpianyi = v.ase_texcoord.xy * dindianpianyi_ST.xy + dindianpianyi_ST.zw;
				float2 panner5_g67 = ( _TimeParameters.x * appendResult8_g67 + uvdindianpianyi);
				float2 appendResult8_g50 = (float2(UVniuquUliudong , UVniuquVliudong));
				float2 uvUVniuquTex = v.ase_texcoord.xy * UVniuquTex_ST.xy + UVniuquTex_ST.zw;
				float2 panner5_g50 = ( _TimeParameters.x * appendResult8_g50 + uvUVniuquTex);
				float2 CenteredUV15_g27 = ( uvUVniuquTex - float2( 0.5,0.5 ) );
				float4 break24_g26 = UVniuqujizuobiaoTiling_Offset;
				float2 break17_g27 = CenteredUV15_g27;
				float2 appendResult23_g27 = (float2(( length( CenteredUV15_g27 ) * break24_g26.x * 2.0 ) , ( atan2( break17_g27.x , break17_g27.y ) * ( 1.0 / TWO_PI ) * break24_g26.y )));
				float2 appendResult10_g26 = (float2(break24_g26.z , break24_g26.w));
				float2 appendResult8_g26 = (float2(UVniuquUliudong , UVniuquVliudong));
				#ifdef JIZUOBIAO_ON
				float2 staticSwitch492 = ( appendResult23_g27 + appendResult10_g26 + ( appendResult8_g26 * _TimeParameters.x ) );
				#else
				float2 staticSwitch492 = panner5_g50;
				#endif
				float4 tex2DNode38 = tex2Dlod( UVniuquTex, float4( staticSwitch492, 0, 0.0) );
				float4 texCoord90 = v.ase_texcoord1;
				texCoord90.xy = v.ase_texcoord1.xy * float2( 1,1 ) + float2( 0,0 );
				float niuqu_Custom1Z154 = texCoord90.z;
				float2 appendResult8_g51 = (float2(zhezhaoUliudong1 , zhezhaoVliudong1));
				float2 uvzhezhaoTex2 = v.ase_texcoord.xy * zhezhaoTex2_ST.xy + zhezhaoTex2_ST.zw;
				float2 panner5_g51 = ( _TimeParameters.x * appendResult8_g51 + uvzhezhaoTex2);
				float4 tex2DNode170 = tex2Dlod( zhezhaoTex2, float4( panner5_g51, 0, 0.0) );
				#ifdef KAIQIZHEZHAO_ON
				float staticSwitch547 = tex2DNode170.r;
				#else
				float staticSwitch547 = 1.0;
				#endif
				#ifdef KAIQINIUQU_ON
				float staticSwitch543 = ( (-0.5 + ((( kaiqiniuqufangxiang )?( ( 1.0 - tex2DNode38.r ) ):( tex2DNode38.r )) - 0.0) * (0.5 - -0.5) / (1.0 - 0.0)) * ( niuqu_Custom1Z154 + UVniuquqiangdu ) * staticSwitch547 );
				#else
				float staticSwitch543 = 0.0;
				#endif
				float UVniuqu296 = staticSwitch543;
				float temp_output_266_0 = (-0.5 + (tex2Dlod( dindianpianyi, float4( ( panner5_g67 + ( UVniuqu296 * dingdianpianyiniuqu ) ), 0, 0.0) ).r - 0.0) * (0.5 - -0.5) / (1.0 - 0.0));
				float2 appendResult8_g70 = (float2(dindianpianyizhezhaoUliudong , dindianpianyizhezhaoVliudong));
				float2 uvdindianpianyizhezhao = v.ase_texcoord.xy * dindianpianyizhezhao_ST.xy + dindianpianyizhezhao_ST.zw;
				float2 panner5_g70 = ( _TimeParameters.x * appendResult8_g70 + uvdindianpianyizhezhao);
				float4 texCoord361 = v.ase_texcoord2;
				texCoord361.xy = v.ase_texcoord2.xy * float2( 1,1 ) + float2( 0,0 );
				#ifdef KAIQIDINGDIANPIANYI_ON
				float3 staticSwitch555 = ( ( ( ( dindianneiwaiqiangdu * temp_output_266_0 ) + ( temp_output_266_0 + dindianpianyigaodu ) ) * tex2Dlod( dindianpianyizhezhao, float4( panner5_g70, 0, 0.0) ).r ) * v.ase_normal * ( dindianpianyiqiangdu + texCoord361.x ) );
				#else
				float3 staticSwitch555 = float3(0,0,0);
				#endif
				
				float3 ase_worldNormal = TransformObjectToWorldNormal(v.ase_normal);
				o.ase_texcoord5.xyz = ase_worldNormal;
				
				float4 ase_clipPos = TransformObjectToHClip((v.vertex).xyz);
				float4 screenPos = ComputeScreenPos(ase_clipPos);
				o.ase_texcoord6 = screenPos;
				
				o.ase_color = v.ase_color;
				o.ase_texcoord3.xy = v.ase_texcoord.xy;
				o.ase_texcoord4 = v.ase_texcoord1;
				
				//setting value to unused interpolator channels and avoid initialization warnings
				o.ase_texcoord3.zw = 0;
				o.ase_texcoord5.w = 0;
				#ifdef ASE_ABSOLUTE_VERTEX_POS
					float3 defaultVertexValue = v.vertex.xyz;
				#else
					float3 defaultVertexValue = float3(0, 0, 0);
				#endif
				float3 vertexValue = staticSwitch555;
				#ifdef ASE_ABSOLUTE_VERTEX_POS
					v.vertex.xyz = vertexValue;
				#else
					v.vertex.xyz += vertexValue;
				#endif
				v.ase_normal = v.ase_normal;

				float3 positionWS = TransformObjectToWorld( v.vertex.xyz );
				float4 positionCS = TransformWorldToHClip( positionWS );

				#if defined(ASE_NEEDS_FRAG_WORLD_POSITION)
				o.worldPos = positionWS;
				#endif
				#if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR) && defined(ASE_NEEDS_FRAG_SHADOWCOORDS)
				VertexPositionInputs vertexInput = (VertexPositionInputs)0;
				vertexInput.positionWS = positionWS;
				vertexInput.positionCS = positionCS;
				o.shadowCoord = GetShadowCoord( vertexInput );
				#endif
				#ifdef ASE_FOG
				o.fogFactor = ComputeFogFactor( positionCS.z );
				#endif
				o.clipPos = positionCS;
				return o;
			}

			#if defined(TESSELLATION_ON)
			struct VertexControl
			{
				float4 vertex : INTERNALTESSPOS;
				float3 ase_normal : NORMAL;
				float4 ase_texcoord : TEXCOORD0;
				float4 ase_texcoord1 : TEXCOORD1;
				float4 ase_texcoord2 : TEXCOORD2;
				float4 ase_color : COLOR;

				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct TessellationFactors
			{
				float edge[3] : SV_TessFactor;
				float inside : SV_InsideTessFactor;
			};

			VertexControl vert ( VertexInput v )
			{
				VertexControl o;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_TRANSFER_INSTANCE_ID(v, o);
				o.vertex = v.vertex;
				o.ase_normal = v.ase_normal;
				o.ase_texcoord = v.ase_texcoord;
				o.ase_texcoord1 = v.ase_texcoord1;
				o.ase_texcoord2 = v.ase_texcoord2;
				o.ase_color = v.ase_color;
				return o;
			}

			TessellationFactors TessellationFunction (InputPatch<VertexControl,3> v)
			{
				TessellationFactors o;
				float4 tf = 1;
				float tessValue = _TessValue; float tessMin = _TessMin; float tessMax = _TessMax;
				float edgeLength = _TessEdgeLength; float tessMaxDisp = _TessMaxDisp;
				#if defined(ASE_FIXED_TESSELLATION)
				tf = FixedTess( tessValue );
				#elif defined(ASE_DISTANCE_TESSELLATION)
				tf = DistanceBasedTess(v[0].vertex, v[1].vertex, v[2].vertex, tessValue, tessMin, tessMax, GetObjectToWorldMatrix(), _WorldSpaceCameraPos );
				#elif defined(ASE_LENGTH_TESSELLATION)
				tf = EdgeLengthBasedTess(v[0].vertex, v[1].vertex, v[2].vertex, edgeLength, GetObjectToWorldMatrix(), _WorldSpaceCameraPos, _ScreenParams );
				#elif defined(ASE_LENGTH_CULL_TESSELLATION)
				tf = EdgeLengthBasedTessCull(v[0].vertex, v[1].vertex, v[2].vertex, edgeLength, tessMaxDisp, GetObjectToWorldMatrix(), _WorldSpaceCameraPos, _ScreenParams, unity_CameraWorldClipPlanes );
				#endif
				o.edge[0] = tf.x; o.edge[1] = tf.y; o.edge[2] = tf.z; o.inside = tf.w;
				return o;
			}

			[domain("tri")]
			[partitioning("fractional_odd")]
			[outputtopology("triangle_cw")]
			[patchconstantfunc("TessellationFunction")]
			[outputcontrolpoints(3)]
			VertexControl HullFunction(InputPatch<VertexControl, 3> patch, uint id : SV_OutputControlPointID)
			{
			   return patch[id];
			}

			[domain("tri")]
			VertexOutput DomainFunction(TessellationFactors factors, OutputPatch<VertexControl, 3> patch, float3 bary : SV_DomainLocation)
			{
				VertexInput o = (VertexInput) 0;
				o.vertex = patch[0].vertex * bary.x + patch[1].vertex * bary.y + patch[2].vertex * bary.z;
				o.ase_normal = patch[0].ase_normal * bary.x + patch[1].ase_normal * bary.y + patch[2].ase_normal * bary.z;
				o.ase_texcoord = patch[0].ase_texcoord * bary.x + patch[1].ase_texcoord * bary.y + patch[2].ase_texcoord * bary.z;
				o.ase_texcoord1 = patch[0].ase_texcoord1 * bary.x + patch[1].ase_texcoord1 * bary.y + patch[2].ase_texcoord1 * bary.z;
				o.ase_texcoord2 = patch[0].ase_texcoord2 * bary.x + patch[1].ase_texcoord2 * bary.y + patch[2].ase_texcoord2 * bary.z;
				o.ase_color = patch[0].ase_color * bary.x + patch[1].ase_color * bary.y + patch[2].ase_color * bary.z;
				#if defined(ASE_PHONG_TESSELLATION)
				float3 pp[3];
				for (int i = 0; i < 3; ++i)
					pp[i] = o.vertex.xyz - patch[i].ase_normal * (dot(o.vertex.xyz, patch[i].ase_normal) - dot(patch[i].vertex.xyz, patch[i].ase_normal));
				float phongStrength = _TessPhongStrength;
				o.vertex.xyz = phongStrength * (pp[0]*bary.x + pp[1]*bary.y + pp[2]*bary.z) + (1.0f-phongStrength) * o.vertex.xyz;
				#endif
				UNITY_TRANSFER_INSTANCE_ID(patch[0], o);
				return VertexFunction(o);
			}
			#else
			VertexOutput vert ( VertexInput v )
			{
				return VertexFunction( v );
			}
			#endif

			half4 frag ( VertexOutput IN  ) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID( IN );
				UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX( IN );

				#if defined(ASE_NEEDS_FRAG_WORLD_POSITION)
				float3 WorldPosition = IN.worldPos;
				#endif
				float4 ShadowCoords = float4( 0, 0, 0, 0 );

				#if defined(ASE_NEEDS_FRAG_SHADOWCOORDS)
					#if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR)
						ShadowCoords = IN.shadowCoord;
					#elif defined(MAIN_LIGHT_CALCULATE_SHADOWS)
						ShadowCoords = TransformWorldToShadowCoord( WorldPosition );
					#endif
				#endif
				float3 temp_output_141_0 = (IN.ase_color).rgb;
				float3 temp_output_137_0 = (zhuwenli_color).rgb;
				float2 appendResult8_g61 = (float2(zhuwenliUliudong , zhuwenliVliudong));
				float2 uvzhuwenli = IN.ase_texcoord3.xy * zhuwenli_ST.xy + zhuwenli_ST.zw;
				float4 texCoord90 = IN.ase_texcoord4;
				texCoord90.xy = IN.ase_texcoord4.xy * float2( 1,1 ) + float2( 0,0 );
				float2 appendResult94 = (float2(texCoord90.x , texCoord90.y));
				float2 panner5_g61 = ( _TimeParameters.x * appendResult8_g61 + ( uvzhuwenli + appendResult94 ));
				float2 CenteredUV15_g63 = ( uvzhuwenli - float2( 0.5,0.5 ) );
				float4 appendResult438 = (float4(zhuwenlijizuobiaoTiling_Offset.x , zhuwenlijizuobiaoTiling_Offset.y , ( texCoord90.x + zhuwenlijizuobiaoTiling_Offset.z ) , ( texCoord90.y + zhuwenlijizuobiaoTiling_Offset.w )));
				float4 break24_g62 = appendResult438;
				float2 break17_g63 = CenteredUV15_g63;
				float2 appendResult23_g63 = (float2(( length( CenteredUV15_g63 ) * break24_g62.x * 2.0 ) , ( atan2( break17_g63.x , break17_g63.y ) * ( 1.0 / TWO_PI ) * break24_g62.y )));
				float2 appendResult10_g62 = (float2(break24_g62.z , break24_g62.w));
				float2 appendResult8_g62 = (float2(zhuwenliUliudong , zhuwenliVliudong));
				#ifdef JIZUOBIAO_ON
				float2 staticSwitch491 = ( appendResult23_g63 + appendResult10_g62 + ( appendResult8_g62 * _TimeParameters.x ) );
				#else
				float2 staticSwitch491 = panner5_g61;
				#endif
				float temp_output_83_0 = (staticSwitch491).x;
				float temp_output_84_0 = (staticSwitch491).y;
				float2 appendResult89 = (float2((( zhuwenliUClamp )?( saturate( temp_output_83_0 ) ):( temp_output_83_0 )) , (( zhuwenliVClamp1 )?( saturate( temp_output_84_0 ) ):( temp_output_84_0 ))));
				float2 appendResult8_g50 = (float2(UVniuquUliudong , UVniuquVliudong));
				float2 uvUVniuquTex = IN.ase_texcoord3.xy * UVniuquTex_ST.xy + UVniuquTex_ST.zw;
				float2 panner5_g50 = ( _TimeParameters.x * appendResult8_g50 + uvUVniuquTex);
				float2 CenteredUV15_g27 = ( uvUVniuquTex - float2( 0.5,0.5 ) );
				float4 break24_g26 = UVniuqujizuobiaoTiling_Offset;
				float2 break17_g27 = CenteredUV15_g27;
				float2 appendResult23_g27 = (float2(( length( CenteredUV15_g27 ) * break24_g26.x * 2.0 ) , ( atan2( break17_g27.x , break17_g27.y ) * ( 1.0 / TWO_PI ) * break24_g26.y )));
				float2 appendResult10_g26 = (float2(break24_g26.z , break24_g26.w));
				float2 appendResult8_g26 = (float2(UVniuquUliudong , UVniuquVliudong));
				#ifdef JIZUOBIAO_ON
				float2 staticSwitch492 = ( appendResult23_g27 + appendResult10_g26 + ( appendResult8_g26 * _TimeParameters.x ) );
				#else
				float2 staticSwitch492 = panner5_g50;
				#endif
				float4 tex2DNode38 = tex2Dlod( UVniuquTex, float4( staticSwitch492, 0, 0.0) );
				float niuqu_Custom1Z154 = texCoord90.z;
				float2 appendResult8_g51 = (float2(zhezhaoUliudong1 , zhezhaoVliudong1));
				float2 uvzhezhaoTex2 = IN.ase_texcoord3.xy * zhezhaoTex2_ST.xy + zhezhaoTex2_ST.zw;
				float2 panner5_g51 = ( _TimeParameters.x * appendResult8_g51 + uvzhezhaoTex2);
				float4 tex2DNode170 = tex2D( zhezhaoTex2, panner5_g51 );
				#ifdef KAIQIZHEZHAO_ON
				float staticSwitch547 = tex2DNode170.r;
				#else
				float staticSwitch547 = 1.0;
				#endif
				#ifdef KAIQINIUQU_ON
				float staticSwitch543 = ( (-0.5 + ((( kaiqiniuqufangxiang )?( ( 1.0 - tex2DNode38.r ) ):( tex2DNode38.r )) - 0.0) * (0.5 - -0.5) / (1.0 - 0.0)) * ( niuqu_Custom1Z154 + UVniuquqiangdu ) * staticSwitch547 );
				#else
				float staticSwitch543 = 0.0;
				#endif
				float UVniuqu296 = staticSwitch543;
				float4 tex2DNode5 = tex2Dlod( zhuwenli, float4( ( appendResult89 + ( UVniuqu296 * zhuwenliniuqu ) ), 0, 0.0) );
				float4 temp_cast_0 = (0.0).xxxx;
				float2 appendResult8_g73 = (float2(fujiatietuUliudong , fujiatietuVliudong));
				float2 uvfujiatietu = IN.ase_texcoord3.xy * fujiatietu_ST.xy + fujiatietu_ST.zw;
				float2 panner5_g73 = ( _TimeParameters.x * appendResult8_g73 + uvfujiatietu);
				float2 CenteredUV15_g72 = ( uvfujiatietu - float2( 0.5,0.5 ) );
				float4 break24_g71 = fujiatietujizuobiaoTiling_Offset1;
				float2 break17_g72 = CenteredUV15_g72;
				float2 appendResult23_g72 = (float2(( length( CenteredUV15_g72 ) * break24_g71.x * 2.0 ) , ( atan2( break17_g72.x , break17_g72.y ) * ( 1.0 / TWO_PI ) * break24_g71.y )));
				float2 appendResult10_g71 = (float2(break24_g71.z , break24_g71.w));
				float2 appendResult8_g71 = (float2(fujiatietuUliudong , fujiatietuVliudong));
				#ifdef JIZUOBIAO_ON
				float2 staticSwitch493 = ( appendResult23_g72 + appendResult10_g71 + ( appendResult8_g71 * _TimeParameters.x ) );
				#else
				float2 staticSwitch493 = panner5_g73;
				#endif
				#ifdef KAIQIFUJIATIETU_ON
				float4 staticSwitch541 = ( tex2Dlod( fujiatietu, float4( ( staticSwitch493 + ( UVniuqu296 * fujiatietuniuqu ) ), 0, 0.0) ) * fujiatietuyanse );
				#else
				float4 staticSwitch541 = temp_cast_0;
				#endif
				#if defined(FUJIATIETUDIEJIAMOSHI_ADD)
				float4 staticSwitch385 = ( tex2DNode5 + staticSwitch541 );
				#elif defined(FUJIATIETUDIEJIAMOSHI_MULTIPLY)
				float4 staticSwitch385 = ( staticSwitch541 * tex2DNode5 );
				#else
				float4 staticSwitch385 = ( tex2DNode5 + staticSwitch541 );
				#endif
				float4 temp_cast_1 = (0.0).xxxx;
				float2 appendResult8_g55 = (float2(rongjieUliudong , rongjieVliudong));
				float2 uvrongjietietu = IN.ase_texcoord3.xy * rongjietietu_ST.xy + rongjietietu_ST.zw;
				float2 panner5_g55 = ( _TimeParameters.x * appendResult8_g55 + uvrongjietietu);
				float2 CenteredUV15_g57 = ( uvrongjietietu - float2( 0.5,0.5 ) );
				float4 break24_g56 = rongjiejizuobiaoTiling_Offset;
				float2 break17_g57 = CenteredUV15_g57;
				float2 appendResult23_g57 = (float2(( length( CenteredUV15_g57 ) * break24_g56.x * 2.0 ) , ( atan2( break17_g57.x , break17_g57.y ) * ( 1.0 / TWO_PI ) * break24_g56.y )));
				float2 appendResult10_g56 = (float2(break24_g56.z , break24_g56.w));
				float2 appendResult8_g56 = (float2(rongjieUliudong , rongjieVliudong));
				#ifdef JIZUOBIAO_ON
				float2 staticSwitch494 = ( appendResult23_g57 + appendResult10_g56 + ( appendResult8_g56 * _TimeParameters.x ) );
				#else
				float2 staticSwitch494 = panner5_g55;
				#endif
				float3 desaturateInitialColor563 = tex2Dlod( rongjietietu, float4( ( staticSwitch494 + ( UVniuqu296 * rongjieniuqu ) ), 0, 0.0) ).rgb;
				float desaturateDot563 = dot( desaturateInitialColor563, float3( 0.299, 0.587, 0.114 ));
				float3 desaturateVar563 = lerp( desaturateInitialColor563, desaturateDot563.xxx, 1.0 );
				float rongjiejindu_Custom1W229 = texCoord90.w;
				float clampResult209 = clamp( ( ((( kaiqirongjiefanx )?( ( 1.0 - desaturateVar563 ) ):( desaturateVar563 ))).x + 1.0 + ( ( rongjiejindu_Custom1W229 + rongjiejindu ) * -2.0 ) ) , 0.0 , 1.0 );
				float smoothstepResult210 = smoothstep( rongjieruanyingdu , ( 1.0 - rongjieruanyingdu ) , clampResult209);
				float zhuwenliA307 = tex2DNode5.a;
				#ifdef KAIQIRONGJIE_ON
				float4 staticSwitch553 = ( ( step( rongjiemiaobianfanwei , ( smoothstepResult210 + rongjiemiaobiankuangdu ) ) - step( rongjiemiaobianfanwei , smoothstepResult210 ) ) * zhuwenliA307 * rongjieliangbianyanse );
				#else
				float4 staticSwitch553 = temp_cast_1;
				#endif
				float4 rongjiekuangduyanse304 = staticSwitch553;
				float3 temp_output_138_0 = (( staticSwitch385 + rongjiekuangduyanse304 )).rgb;
				float3 ase_worldNormal = IN.ase_texcoord5.xyz;
				float3 ase_worldViewDir = ( _WorldSpaceCameraPos.xyz - WorldPosition );
				ase_worldViewDir = normalize(ase_worldViewDir);
				float dotResult317 = dot( ase_worldNormal , ase_worldViewDir );
				float temp_output_318_0 = abs( dotResult317 );
				float4 temp_output_326_0 = ( ( saturate( pow( (( fanxiangfeinier )?( temp_output_318_0 ):( ( 1.0 - temp_output_318_0 ) )) , feinierruihua ) ) * feinierqiangdu ) * feinieryanse );
				float3 temp_output_328_0 = (temp_output_326_0).rgb;
				#if defined(FEINIERMOSHI_ADD)
				float3 staticSwitch350 = ( temp_output_328_0 + ( temp_output_137_0 * temp_output_138_0 * temp_output_141_0 ) );
				#elif defined(FEINIERMOSHI_MULTIPLY)
				float3 staticSwitch350 = ( temp_output_141_0 * temp_output_137_0 * temp_output_138_0 * temp_output_328_0 );
				#else
				float3 staticSwitch350 = ( temp_output_328_0 + ( temp_output_137_0 * temp_output_138_0 * temp_output_141_0 ) );
				#endif
				#ifdef KAIQIFEINIER_ON
				float3 staticSwitch359 = staticSwitch350;
				#else
				float3 staticSwitch359 = ( temp_output_141_0 * temp_output_137_0 * temp_output_138_0 );
				#endif
				
				float2 appendResult8_g66 = (float2(zhezhaoUliudong , zhezhaoVliudong));
				float2 uvzhezhaoTex = IN.ase_texcoord3.xy * zhezhaoTex_ST.xy + zhezhaoTex_ST.zw;
				float2 panner5_g66 = ( _TimeParameters.x * appendResult8_g66 + uvzhezhaoTex);
				float2 CenteredUV15_g69 = ( uvzhezhaoTex - float2( 0.5,0.5 ) );
				float4 break24_g68 = zhezhaojizuobiaoTiling_Offset1;
				float2 break17_g69 = CenteredUV15_g69;
				float2 appendResult23_g69 = (float2(( length( CenteredUV15_g69 ) * break24_g68.x * 2.0 ) , ( atan2( break17_g69.x , break17_g69.y ) * ( 1.0 / TWO_PI ) * break24_g68.y )));
				float2 appendResult10_g68 = (float2(break24_g68.z , break24_g68.w));
				float2 appendResult8_g68 = (float2(zhezhaoUliudong , zhezhaoVliudong));
				#ifdef JIZUOBIAO_ON
				float2 staticSwitch496 = ( appendResult23_g69 + appendResult10_g68 + ( appendResult8_g68 * _TimeParameters.x ) );
				#else
				float2 staticSwitch496 = panner5_g66;
				#endif
				float4 tex2DNode26 = tex2Dlod( zhezhaoTex, float4( ( staticSwitch496 + ( UVniuqu296 * zhezhaoniuqu ) ), 0, 0.0) );
				#ifdef KAIQIZHEZHAO_ON
				float staticSwitch545 = saturate( ( ( tex2DNode26.r * tex2DNode26.a * tex2DNode170.r ) + zhezhaoqiangdu ) );
				#else
				float staticSwitch545 = 1.0;
				#endif
				#ifdef KAIQIRONGJIE_ON
				float staticSwitch549 = smoothstepResult210;
				#else
				float staticSwitch549 = 1.0;
				#endif
				float rongjie301 = staticSwitch549;
				#ifdef KAIQIFEINIER_ON
				float staticSwitch358 = (temp_output_326_0).a;
				#else
				float staticSwitch358 = 1.0;
				#endif
				float4 screenPos = IN.ase_texcoord6;
				float4 ase_screenPosNorm = screenPos / screenPos.w;
				ase_screenPosNorm.z = ( UNITY_NEAR_CLIP_VALUE >= 0 ) ? ase_screenPosNorm.z : ase_screenPosNorm.z * 0.5 + 0.5;
				float screenDepth364 = LinearEyeDepth(SHADERGRAPH_SAMPLE_SCENE_DEPTH( ase_screenPosNorm.xy ),_ZBufferParams);
				float distanceDepth364 = saturate( abs( ( screenDepth364 - LinearEyeDepth( ase_screenPosNorm.z,_ZBufferParams ) ) / ( DepthFade ) ) );
				#ifdef KAIQIRUANLIZI_ON
				float staticSwitch557 = saturate( distanceDepth364 );
				#else
				float staticSwitch557 = 1.0;
				#endif
				
				float3 BakedAlbedo = 0;
				float3 BakedEmission = 0;
				float3 Color = staticSwitch359;
				float Alpha = ( (( shiyongRtongdao )?( tex2DNode5.r ):( tex2DNode5.a )) * zhuwenli_color.a * zhengtitoumingdu * staticSwitch545 * rongjie301 * staticSwitch358 * IN.ase_color.a * staticSwitch557 );
				float AlphaClipThreshold = 0.5;
				float AlphaClipThresholdShadow = 0.5;

				#ifdef _ALPHATEST_ON
					clip( Alpha - AlphaClipThreshold );
				#endif

				#ifdef LOD_FADE_CROSSFADE
					LODDitheringTransition( IN.clipPos.xyz, unity_LODFade.x );
				#endif

				#ifdef ASE_FOG
					Color = MixFog( Color, IN.fogFactor );
				#endif

				return half4( Color, Alpha );
			}

			ENDHLSL
		}

		
		Pass
		{
			
			Name "DepthOnly"
			Tags { "LightMode"="DepthOnly" }

			ZWrite On
			ColorMask 0
			AlphaToMask Off

			HLSLPROGRAM
			#define _RECEIVE_SHADOWS_OFF 1
			#pragma multi_compile_instancing
			#define ASE_SRP_VERSION 100202
			#define REQUIRE_DEPTH_TEXTURE 1

			#pragma prefer_hlslcc gles
			#pragma exclude_renderers d3d11_9x

			#pragma vertex vert
			#pragma fragment frag

			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderGraphFunctions.hlsl"
			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"

			#define ASE_NEEDS_VERT_NORMAL
			#define ASE_NEEDS_FRAG_WORLD_POSITION
			#pragma shader_feature_local KAIQIDINGDIANPIANYI_ON
			#pragma shader_feature_local KAIQINIUQU_ON
			#pragma shader_feature_local JIZUOBIAO_ON
			#pragma shader_feature_local KAIQIZHEZHAO_ON
			#pragma shader_feature_local KAIQIRONGJIE_ON
			#pragma shader_feature_local KAIQIFEINIER_ON
			#pragma shader_feature_local KAIQIRUANLIZI_ON


			struct VertexInput
			{
				float4 vertex : POSITION;
				float3 ase_normal : NORMAL;
				float4 ase_texcoord : TEXCOORD0;
				float4 ase_texcoord1 : TEXCOORD1;
				float4 ase_texcoord2 : TEXCOORD2;
				float4 ase_color : COLOR;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct VertexOutput
			{
				float4 clipPos : SV_POSITION;
				#if defined(ASE_NEEDS_FRAG_WORLD_POSITION)
				float3 worldPos : TEXCOORD0;
				#endif
				#if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR) && defined(ASE_NEEDS_FRAG_SHADOWCOORDS)
				float4 shadowCoord : TEXCOORD1;
				#endif
				float4 ase_texcoord2 : TEXCOORD2;
				float4 ase_texcoord3 : TEXCOORD3;
				float4 ase_texcoord4 : TEXCOORD4;
				float4 ase_color : COLOR;
				float4 ase_texcoord5 : TEXCOORD5;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};

			CBUFFER_START(UnityPerMaterial)
			float4 zhezhaoTex2_ST;
			float4 fujiatietuyanse;
			float4 zhuwenlijizuobiaoTiling_Offset;
			float4 zhuwenli_ST;
			float4 zhuwenli_color;
			float4 dindianpianyizhezhao_ST;
			float4 rongjietietu_ST;
			float4 rongjiejizuobiaoTiling_Offset;
			float4 fujiatietujizuobiaoTiling_Offset1;
			float4 rongjieliangbianyanse;
			float4 UVniuqujizuobiaoTiling_Offset;
			float4 UVniuquTex_ST;
			float4 fujiatietu_ST;
			float4 feinieryanse;
			float4 dindianpianyi_ST;
			float4 zhezhaojizuobiaoTiling_Offset1;
			float4 zhezhaoTex_ST;
			float feinierqiangdu;
			float fujiatietuniuqu;
			float zhezhaoniuqu;
			half rongjiemiaobianfanwei;
			half rongjieruanyingdu;
			float kaiqirongjiefanx;
			float rongjieUliudong;
			float rongjieVliudong;
			float feinierruihua;
			float zhezhaoUliudong;
			float zhengtitoumingdu;
			float shiyongRtongdao;
			float rongjieniuqu;
			float rongjiejindu;
			half rongjiemiaobiankuangdu;
			float fanxiangfeinier;
			float zhezhaoVliudong;
			float Src;
			float zhuwenliVClamp1;
			float fujiatietuUliudong;
			float Dst;
			float Cullmode;
			float shenduceshi;
			float shendu;
			float dindianneiwaiqiangdu;
			float dindianpianyiUliudong;
			float dindianpianyiVliudong;
			float kaiqiniuqufangxiang;
			float UVniuquUliudong;
			float UVniuquVliudong;
			float UVniuquqiangdu;
			float zhezhaoUliudong1;
			float zhezhaoVliudong1;
			float dingdianpianyiniuqu;
			float dindianpianyigaodu;
			float dindianpianyizhezhaoUliudong;
			float dindianpianyizhezhaoVliudong;
			float dindianpianyiqiangdu;
			float zhuwenliUClamp;
			float zhuwenliUliudong;
			float zhuwenliVliudong;
			float zhezhaoqiangdu;
			float zhuwenliniuqu;
			float fujiatietuVliudong;
			float DepthFade;
			#ifdef TESSELLATION_ON
				float _TessPhongStrength;
				float _TessValue;
				float _TessMin;
				float _TessMax;
				float _TessEdgeLength;
				float _TessMaxDisp;
			#endif
			CBUFFER_END
			sampler2D dindianpianyi;
			sampler2D UVniuquTex;
			sampler2D zhezhaoTex2;
			sampler2D dindianpianyizhezhao;
			sampler2D zhuwenli;
			sampler2D zhezhaoTex;
			sampler2D rongjietietu;
			uniform float4 _CameraDepthTexture_TexelSize;


			
			VertexOutput VertexFunction( VertexInput v  )
			{
				VertexOutput o = (VertexOutput)0;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_TRANSFER_INSTANCE_ID(v, o);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);

				float2 appendResult8_g67 = (float2(dindianpianyiUliudong , dindianpianyiVliudong));
				float2 uvdindianpianyi = v.ase_texcoord.xy * dindianpianyi_ST.xy + dindianpianyi_ST.zw;
				float2 panner5_g67 = ( _TimeParameters.x * appendResult8_g67 + uvdindianpianyi);
				float2 appendResult8_g50 = (float2(UVniuquUliudong , UVniuquVliudong));
				float2 uvUVniuquTex = v.ase_texcoord.xy * UVniuquTex_ST.xy + UVniuquTex_ST.zw;
				float2 panner5_g50 = ( _TimeParameters.x * appendResult8_g50 + uvUVniuquTex);
				float2 CenteredUV15_g27 = ( uvUVniuquTex - float2( 0.5,0.5 ) );
				float4 break24_g26 = UVniuqujizuobiaoTiling_Offset;
				float2 break17_g27 = CenteredUV15_g27;
				float2 appendResult23_g27 = (float2(( length( CenteredUV15_g27 ) * break24_g26.x * 2.0 ) , ( atan2( break17_g27.x , break17_g27.y ) * ( 1.0 / TWO_PI ) * break24_g26.y )));
				float2 appendResult10_g26 = (float2(break24_g26.z , break24_g26.w));
				float2 appendResult8_g26 = (float2(UVniuquUliudong , UVniuquVliudong));
				#ifdef JIZUOBIAO_ON
				float2 staticSwitch492 = ( appendResult23_g27 + appendResult10_g26 + ( appendResult8_g26 * _TimeParameters.x ) );
				#else
				float2 staticSwitch492 = panner5_g50;
				#endif
				float4 tex2DNode38 = tex2Dlod( UVniuquTex, float4( staticSwitch492, 0, 0.0) );
				float4 texCoord90 = v.ase_texcoord1;
				texCoord90.xy = v.ase_texcoord1.xy * float2( 1,1 ) + float2( 0,0 );
				float niuqu_Custom1Z154 = texCoord90.z;
				float2 appendResult8_g51 = (float2(zhezhaoUliudong1 , zhezhaoVliudong1));
				float2 uvzhezhaoTex2 = v.ase_texcoord.xy * zhezhaoTex2_ST.xy + zhezhaoTex2_ST.zw;
				float2 panner5_g51 = ( _TimeParameters.x * appendResult8_g51 + uvzhezhaoTex2);
				float4 tex2DNode170 = tex2Dlod( zhezhaoTex2, float4( panner5_g51, 0, 0.0) );
				#ifdef KAIQIZHEZHAO_ON
				float staticSwitch547 = tex2DNode170.r;
				#else
				float staticSwitch547 = 1.0;
				#endif
				#ifdef KAIQINIUQU_ON
				float staticSwitch543 = ( (-0.5 + ((( kaiqiniuqufangxiang )?( ( 1.0 - tex2DNode38.r ) ):( tex2DNode38.r )) - 0.0) * (0.5 - -0.5) / (1.0 - 0.0)) * ( niuqu_Custom1Z154 + UVniuquqiangdu ) * staticSwitch547 );
				#else
				float staticSwitch543 = 0.0;
				#endif
				float UVniuqu296 = staticSwitch543;
				float temp_output_266_0 = (-0.5 + (tex2Dlod( dindianpianyi, float4( ( panner5_g67 + ( UVniuqu296 * dingdianpianyiniuqu ) ), 0, 0.0) ).r - 0.0) * (0.5 - -0.5) / (1.0 - 0.0));
				float2 appendResult8_g70 = (float2(dindianpianyizhezhaoUliudong , dindianpianyizhezhaoVliudong));
				float2 uvdindianpianyizhezhao = v.ase_texcoord.xy * dindianpianyizhezhao_ST.xy + dindianpianyizhezhao_ST.zw;
				float2 panner5_g70 = ( _TimeParameters.x * appendResult8_g70 + uvdindianpianyizhezhao);
				float4 texCoord361 = v.ase_texcoord2;
				texCoord361.xy = v.ase_texcoord2.xy * float2( 1,1 ) + float2( 0,0 );
				#ifdef KAIQIDINGDIANPIANYI_ON
				float3 staticSwitch555 = ( ( ( ( dindianneiwaiqiangdu * temp_output_266_0 ) + ( temp_output_266_0 + dindianpianyigaodu ) ) * tex2Dlod( dindianpianyizhezhao, float4( panner5_g70, 0, 0.0) ).r ) * v.ase_normal * ( dindianpianyiqiangdu + texCoord361.x ) );
				#else
				float3 staticSwitch555 = float3(0,0,0);
				#endif
				
				float3 ase_worldNormal = TransformObjectToWorldNormal(v.ase_normal);
				o.ase_texcoord4.xyz = ase_worldNormal;
				float4 ase_clipPos = TransformObjectToHClip((v.vertex).xyz);
				float4 screenPos = ComputeScreenPos(ase_clipPos);
				o.ase_texcoord5 = screenPos;
				
				o.ase_texcoord2.xy = v.ase_texcoord.xy;
				o.ase_texcoord3 = v.ase_texcoord1;
				o.ase_color = v.ase_color;
				
				//setting value to unused interpolator channels and avoid initialization warnings
				o.ase_texcoord2.zw = 0;
				o.ase_texcoord4.w = 0;
				#ifdef ASE_ABSOLUTE_VERTEX_POS
					float3 defaultVertexValue = v.vertex.xyz;
				#else
					float3 defaultVertexValue = float3(0, 0, 0);
				#endif
				float3 vertexValue = staticSwitch555;
				#ifdef ASE_ABSOLUTE_VERTEX_POS
					v.vertex.xyz = vertexValue;
				#else
					v.vertex.xyz += vertexValue;
				#endif

				v.ase_normal = v.ase_normal;

				float3 positionWS = TransformObjectToWorld( v.vertex.xyz );

				#if defined(ASE_NEEDS_FRAG_WORLD_POSITION)
				o.worldPos = positionWS;
				#endif

				o.clipPos = TransformWorldToHClip( positionWS );
				#if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR) && defined(ASE_NEEDS_FRAG_SHADOWCOORDS)
					VertexPositionInputs vertexInput = (VertexPositionInputs)0;
					vertexInput.positionWS = positionWS;
					vertexInput.positionCS = clipPos;
					o.shadowCoord = GetShadowCoord( vertexInput );
				#endif
				return o;
			}

			#if defined(TESSELLATION_ON)
			struct VertexControl
			{
				float4 vertex : INTERNALTESSPOS;
				float3 ase_normal : NORMAL;
				float4 ase_texcoord : TEXCOORD0;
				float4 ase_texcoord1 : TEXCOORD1;
				float4 ase_texcoord2 : TEXCOORD2;
				float4 ase_color : COLOR;

				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct TessellationFactors
			{
				float edge[3] : SV_TessFactor;
				float inside : SV_InsideTessFactor;
			};

			VertexControl vert ( VertexInput v )
			{
				VertexControl o;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_TRANSFER_INSTANCE_ID(v, o);
				o.vertex = v.vertex;
				o.ase_normal = v.ase_normal;
				o.ase_texcoord = v.ase_texcoord;
				o.ase_texcoord1 = v.ase_texcoord1;
				o.ase_texcoord2 = v.ase_texcoord2;
				o.ase_color = v.ase_color;
				return o;
			}

			TessellationFactors TessellationFunction (InputPatch<VertexControl,3> v)
			{
				TessellationFactors o;
				float4 tf = 1;
				float tessValue = _TessValue; float tessMin = _TessMin; float tessMax = _TessMax;
				float edgeLength = _TessEdgeLength; float tessMaxDisp = _TessMaxDisp;
				#if defined(ASE_FIXED_TESSELLATION)
				tf = FixedTess( tessValue );
				#elif defined(ASE_DISTANCE_TESSELLATION)
				tf = DistanceBasedTess(v[0].vertex, v[1].vertex, v[2].vertex, tessValue, tessMin, tessMax, GetObjectToWorldMatrix(), _WorldSpaceCameraPos );
				#elif defined(ASE_LENGTH_TESSELLATION)
				tf = EdgeLengthBasedTess(v[0].vertex, v[1].vertex, v[2].vertex, edgeLength, GetObjectToWorldMatrix(), _WorldSpaceCameraPos, _ScreenParams );
				#elif defined(ASE_LENGTH_CULL_TESSELLATION)
				tf = EdgeLengthBasedTessCull(v[0].vertex, v[1].vertex, v[2].vertex, edgeLength, tessMaxDisp, GetObjectToWorldMatrix(), _WorldSpaceCameraPos, _ScreenParams, unity_CameraWorldClipPlanes );
				#endif
				o.edge[0] = tf.x; o.edge[1] = tf.y; o.edge[2] = tf.z; o.inside = tf.w;
				return o;
			}

			[domain("tri")]
			[partitioning("fractional_odd")]
			[outputtopology("triangle_cw")]
			[patchconstantfunc("TessellationFunction")]
			[outputcontrolpoints(3)]
			VertexControl HullFunction(InputPatch<VertexControl, 3> patch, uint id : SV_OutputControlPointID)
			{
			   return patch[id];
			}

			[domain("tri")]
			VertexOutput DomainFunction(TessellationFactors factors, OutputPatch<VertexControl, 3> patch, float3 bary : SV_DomainLocation)
			{
				VertexInput o = (VertexInput) 0;
				o.vertex = patch[0].vertex * bary.x + patch[1].vertex * bary.y + patch[2].vertex * bary.z;
				o.ase_normal = patch[0].ase_normal * bary.x + patch[1].ase_normal * bary.y + patch[2].ase_normal * bary.z;
				o.ase_texcoord = patch[0].ase_texcoord * bary.x + patch[1].ase_texcoord * bary.y + patch[2].ase_texcoord * bary.z;
				o.ase_texcoord1 = patch[0].ase_texcoord1 * bary.x + patch[1].ase_texcoord1 * bary.y + patch[2].ase_texcoord1 * bary.z;
				o.ase_texcoord2 = patch[0].ase_texcoord2 * bary.x + patch[1].ase_texcoord2 * bary.y + patch[2].ase_texcoord2 * bary.z;
				o.ase_color = patch[0].ase_color * bary.x + patch[1].ase_color * bary.y + patch[2].ase_color * bary.z;
				#if defined(ASE_PHONG_TESSELLATION)
				float3 pp[3];
				for (int i = 0; i < 3; ++i)
					pp[i] = o.vertex.xyz - patch[i].ase_normal * (dot(o.vertex.xyz, patch[i].ase_normal) - dot(patch[i].vertex.xyz, patch[i].ase_normal));
				float phongStrength = _TessPhongStrength;
				o.vertex.xyz = phongStrength * (pp[0]*bary.x + pp[1]*bary.y + pp[2]*bary.z) + (1.0f-phongStrength) * o.vertex.xyz;
				#endif
				UNITY_TRANSFER_INSTANCE_ID(patch[0], o);
				return VertexFunction(o);
			}
			#else
			VertexOutput vert ( VertexInput v )
			{
				return VertexFunction( v );
			}
			#endif

			half4 frag(VertexOutput IN  ) : SV_TARGET
			{
				UNITY_SETUP_INSTANCE_ID(IN);
				UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX( IN );

				#if defined(ASE_NEEDS_FRAG_WORLD_POSITION)
				float3 WorldPosition = IN.worldPos;
				#endif
				float4 ShadowCoords = float4( 0, 0, 0, 0 );

				#if defined(ASE_NEEDS_FRAG_SHADOWCOORDS)
					#if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR)
						ShadowCoords = IN.shadowCoord;
					#elif defined(MAIN_LIGHT_CALCULATE_SHADOWS)
						ShadowCoords = TransformWorldToShadowCoord( WorldPosition );
					#endif
				#endif

				float2 appendResult8_g61 = (float2(zhuwenliUliudong , zhuwenliVliudong));
				float2 uvzhuwenli = IN.ase_texcoord2.xy * zhuwenli_ST.xy + zhuwenli_ST.zw;
				float4 texCoord90 = IN.ase_texcoord3;
				texCoord90.xy = IN.ase_texcoord3.xy * float2( 1,1 ) + float2( 0,0 );
				float2 appendResult94 = (float2(texCoord90.x , texCoord90.y));
				float2 panner5_g61 = ( _TimeParameters.x * appendResult8_g61 + ( uvzhuwenli + appendResult94 ));
				float2 CenteredUV15_g63 = ( uvzhuwenli - float2( 0.5,0.5 ) );
				float4 appendResult438 = (float4(zhuwenlijizuobiaoTiling_Offset.x , zhuwenlijizuobiaoTiling_Offset.y , ( texCoord90.x + zhuwenlijizuobiaoTiling_Offset.z ) , ( texCoord90.y + zhuwenlijizuobiaoTiling_Offset.w )));
				float4 break24_g62 = appendResult438;
				float2 break17_g63 = CenteredUV15_g63;
				float2 appendResult23_g63 = (float2(( length( CenteredUV15_g63 ) * break24_g62.x * 2.0 ) , ( atan2( break17_g63.x , break17_g63.y ) * ( 1.0 / TWO_PI ) * break24_g62.y )));
				float2 appendResult10_g62 = (float2(break24_g62.z , break24_g62.w));
				float2 appendResult8_g62 = (float2(zhuwenliUliudong , zhuwenliVliudong));
				#ifdef JIZUOBIAO_ON
				float2 staticSwitch491 = ( appendResult23_g63 + appendResult10_g62 + ( appendResult8_g62 * _TimeParameters.x ) );
				#else
				float2 staticSwitch491 = panner5_g61;
				#endif
				float temp_output_83_0 = (staticSwitch491).x;
				float temp_output_84_0 = (staticSwitch491).y;
				float2 appendResult89 = (float2((( zhuwenliUClamp )?( saturate( temp_output_83_0 ) ):( temp_output_83_0 )) , (( zhuwenliVClamp1 )?( saturate( temp_output_84_0 ) ):( temp_output_84_0 ))));
				float2 appendResult8_g50 = (float2(UVniuquUliudong , UVniuquVliudong));
				float2 uvUVniuquTex = IN.ase_texcoord2.xy * UVniuquTex_ST.xy + UVniuquTex_ST.zw;
				float2 panner5_g50 = ( _TimeParameters.x * appendResult8_g50 + uvUVniuquTex);
				float2 CenteredUV15_g27 = ( uvUVniuquTex - float2( 0.5,0.5 ) );
				float4 break24_g26 = UVniuqujizuobiaoTiling_Offset;
				float2 break17_g27 = CenteredUV15_g27;
				float2 appendResult23_g27 = (float2(( length( CenteredUV15_g27 ) * break24_g26.x * 2.0 ) , ( atan2( break17_g27.x , break17_g27.y ) * ( 1.0 / TWO_PI ) * break24_g26.y )));
				float2 appendResult10_g26 = (float2(break24_g26.z , break24_g26.w));
				float2 appendResult8_g26 = (float2(UVniuquUliudong , UVniuquVliudong));
				#ifdef JIZUOBIAO_ON
				float2 staticSwitch492 = ( appendResult23_g27 + appendResult10_g26 + ( appendResult8_g26 * _TimeParameters.x ) );
				#else
				float2 staticSwitch492 = panner5_g50;
				#endif
				float4 tex2DNode38 = tex2Dlod( UVniuquTex, float4( staticSwitch492, 0, 0.0) );
				float niuqu_Custom1Z154 = texCoord90.z;
				float2 appendResult8_g51 = (float2(zhezhaoUliudong1 , zhezhaoVliudong1));
				float2 uvzhezhaoTex2 = IN.ase_texcoord2.xy * zhezhaoTex2_ST.xy + zhezhaoTex2_ST.zw;
				float2 panner5_g51 = ( _TimeParameters.x * appendResult8_g51 + uvzhezhaoTex2);
				float4 tex2DNode170 = tex2D( zhezhaoTex2, panner5_g51 );
				#ifdef KAIQIZHEZHAO_ON
				float staticSwitch547 = tex2DNode170.r;
				#else
				float staticSwitch547 = 1.0;
				#endif
				#ifdef KAIQINIUQU_ON
				float staticSwitch543 = ( (-0.5 + ((( kaiqiniuqufangxiang )?( ( 1.0 - tex2DNode38.r ) ):( tex2DNode38.r )) - 0.0) * (0.5 - -0.5) / (1.0 - 0.0)) * ( niuqu_Custom1Z154 + UVniuquqiangdu ) * staticSwitch547 );
				#else
				float staticSwitch543 = 0.0;
				#endif
				float UVniuqu296 = staticSwitch543;
				float4 tex2DNode5 = tex2Dlod( zhuwenli, float4( ( appendResult89 + ( UVniuqu296 * zhuwenliniuqu ) ), 0, 0.0) );
				float2 appendResult8_g66 = (float2(zhezhaoUliudong , zhezhaoVliudong));
				float2 uvzhezhaoTex = IN.ase_texcoord2.xy * zhezhaoTex_ST.xy + zhezhaoTex_ST.zw;
				float2 panner5_g66 = ( _TimeParameters.x * appendResult8_g66 + uvzhezhaoTex);
				float2 CenteredUV15_g69 = ( uvzhezhaoTex - float2( 0.5,0.5 ) );
				float4 break24_g68 = zhezhaojizuobiaoTiling_Offset1;
				float2 break17_g69 = CenteredUV15_g69;
				float2 appendResult23_g69 = (float2(( length( CenteredUV15_g69 ) * break24_g68.x * 2.0 ) , ( atan2( break17_g69.x , break17_g69.y ) * ( 1.0 / TWO_PI ) * break24_g68.y )));
				float2 appendResult10_g68 = (float2(break24_g68.z , break24_g68.w));
				float2 appendResult8_g68 = (float2(zhezhaoUliudong , zhezhaoVliudong));
				#ifdef JIZUOBIAO_ON
				float2 staticSwitch496 = ( appendResult23_g69 + appendResult10_g68 + ( appendResult8_g68 * _TimeParameters.x ) );
				#else
				float2 staticSwitch496 = panner5_g66;
				#endif
				float4 tex2DNode26 = tex2Dlod( zhezhaoTex, float4( ( staticSwitch496 + ( UVniuqu296 * zhezhaoniuqu ) ), 0, 0.0) );
				#ifdef KAIQIZHEZHAO_ON
				float staticSwitch545 = saturate( ( ( tex2DNode26.r * tex2DNode26.a * tex2DNode170.r ) + zhezhaoqiangdu ) );
				#else
				float staticSwitch545 = 1.0;
				#endif
				float2 appendResult8_g55 = (float2(rongjieUliudong , rongjieVliudong));
				float2 uvrongjietietu = IN.ase_texcoord2.xy * rongjietietu_ST.xy + rongjietietu_ST.zw;
				float2 panner5_g55 = ( _TimeParameters.x * appendResult8_g55 + uvrongjietietu);
				float2 CenteredUV15_g57 = ( uvrongjietietu - float2( 0.5,0.5 ) );
				float4 break24_g56 = rongjiejizuobiaoTiling_Offset;
				float2 break17_g57 = CenteredUV15_g57;
				float2 appendResult23_g57 = (float2(( length( CenteredUV15_g57 ) * break24_g56.x * 2.0 ) , ( atan2( break17_g57.x , break17_g57.y ) * ( 1.0 / TWO_PI ) * break24_g56.y )));
				float2 appendResult10_g56 = (float2(break24_g56.z , break24_g56.w));
				float2 appendResult8_g56 = (float2(rongjieUliudong , rongjieVliudong));
				#ifdef JIZUOBIAO_ON
				float2 staticSwitch494 = ( appendResult23_g57 + appendResult10_g56 + ( appendResult8_g56 * _TimeParameters.x ) );
				#else
				float2 staticSwitch494 = panner5_g55;
				#endif
				float3 desaturateInitialColor563 = tex2Dlod( rongjietietu, float4( ( staticSwitch494 + ( UVniuqu296 * rongjieniuqu ) ), 0, 0.0) ).rgb;
				float desaturateDot563 = dot( desaturateInitialColor563, float3( 0.299, 0.587, 0.114 ));
				float3 desaturateVar563 = lerp( desaturateInitialColor563, desaturateDot563.xxx, 1.0 );
				float rongjiejindu_Custom1W229 = texCoord90.w;
				float clampResult209 = clamp( ( ((( kaiqirongjiefanx )?( ( 1.0 - desaturateVar563 ) ):( desaturateVar563 ))).x + 1.0 + ( ( rongjiejindu_Custom1W229 + rongjiejindu ) * -2.0 ) ) , 0.0 , 1.0 );
				float smoothstepResult210 = smoothstep( rongjieruanyingdu , ( 1.0 - rongjieruanyingdu ) , clampResult209);
				#ifdef KAIQIRONGJIE_ON
				float staticSwitch549 = smoothstepResult210;
				#else
				float staticSwitch549 = 1.0;
				#endif
				float rongjie301 = staticSwitch549;
				float3 ase_worldNormal = IN.ase_texcoord4.xyz;
				float3 ase_worldViewDir = ( _WorldSpaceCameraPos.xyz - WorldPosition );
				ase_worldViewDir = normalize(ase_worldViewDir);
				float dotResult317 = dot( ase_worldNormal , ase_worldViewDir );
				float temp_output_318_0 = abs( dotResult317 );
				float4 temp_output_326_0 = ( ( saturate( pow( (( fanxiangfeinier )?( temp_output_318_0 ):( ( 1.0 - temp_output_318_0 ) )) , feinierruihua ) ) * feinierqiangdu ) * feinieryanse );
				#ifdef KAIQIFEINIER_ON
				float staticSwitch358 = (temp_output_326_0).a;
				#else
				float staticSwitch358 = 1.0;
				#endif
				float4 screenPos = IN.ase_texcoord5;
				float4 ase_screenPosNorm = screenPos / screenPos.w;
				ase_screenPosNorm.z = ( UNITY_NEAR_CLIP_VALUE >= 0 ) ? ase_screenPosNorm.z : ase_screenPosNorm.z * 0.5 + 0.5;
				float screenDepth364 = LinearEyeDepth(SHADERGRAPH_SAMPLE_SCENE_DEPTH( ase_screenPosNorm.xy ),_ZBufferParams);
				float distanceDepth364 = saturate( abs( ( screenDepth364 - LinearEyeDepth( ase_screenPosNorm.z,_ZBufferParams ) ) / ( DepthFade ) ) );
				#ifdef KAIQIRUANLIZI_ON
				float staticSwitch557 = saturate( distanceDepth364 );
				#else
				float staticSwitch557 = 1.0;
				#endif
				
				float Alpha = ( (( shiyongRtongdao )?( tex2DNode5.r ):( tex2DNode5.a )) * zhuwenli_color.a * zhengtitoumingdu * staticSwitch545 * rongjie301 * staticSwitch358 * IN.ase_color.a * staticSwitch557 );
				float AlphaClipThreshold = 0.5;

				#ifdef _ALPHATEST_ON
					clip(Alpha - AlphaClipThreshold);
				#endif

				#ifdef LOD_FADE_CROSSFADE
					LODDitheringTransition( IN.clipPos.xyz, unity_LODFade.x );
				#endif
				return 0;
			}
			ENDHLSL
		}

	
	}
	// CustomEditor "UnityEditor.ShaderGraph.PBRMasterGUI"
	Fallback "Hidden/InternalErrorShader"
	
}
/*ASEBEGIN
Version=18900
1920;0;1920;1011;1168.406;-2714.39;1.817203;True;True
Node;AmplifyShaderEditor.CommentaryNode;157;-3623.407,375.632;Inherit;False;2862.39;765.7504;;12;296;44;245;487;482;155;429;428;38;465;543;544;UV扭曲;1,1,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;465;-3585,477.7427;Inherit;False;1018.993;618.3058;;7;48;146;50;47;492;526;540;UV流动;1,1,1,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;48;-3472.917,676.9788;Inherit;False;Property;UVniuquUliudong;UV扭曲U流动;37;0;Create;False;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;50;-3475.176,808.6719;Inherit;False;Property;UVniuquVliudong;UV扭曲V流动;38;0;Create;False;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.Vector4Node;146;-3557.404,913.9703;Inherit;False;Property;UVniuqujizuobiaoTiling_Offset;UV扭曲极坐标Tiling_Offset;36;0;Create;False;0;0;0;False;0;False;1,1,0,0;1,1,0,0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureCoordinatesNode;47;-3513.529,526.2993;Inherit;False;0;38;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.FunctionNode;526;-3146.392,823.1361;Inherit;False;CalculatePolarCoordinatesUV;-1;;26;59e6323c63d3ead4f95451b440a41729;0;4;32;FLOAT2;0,0;False;16;FLOAT;0;False;17;FLOAT;0;False;23;FLOAT4;1,1,0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.FunctionNode;540;-3104.181,595.4307;Inherit;False;CalculateUV;-1;;50;878911039a84d384fbe16355229feed8;0;3;32;FLOAT2;0,0;False;16;FLOAT;0;False;17;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.StaticSwitch;492;-2815.059,674.124;Inherit;False;Property;_Keyword1;Keyword 1;11;0;Create;True;0;0;0;False;0;False;0;0;0;True;;Toggle;2;Key0;Key1;Reference;491;True;True;9;1;FLOAT2;0,0;False;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT2;0,0;False;6;FLOAT2;0,0;False;7;FLOAT2;0,0;False;8;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.CommentaryNode;186;-3979.441,1312.888;Inherit;False;1465.638;469.1775;;7;547;170;548;533;171;174;172;遮罩2;1,1,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;135;-4939.201,418.8468;Inherit;False;630.2683;473.902;;4;229;154;94;90;自定义数据1;1,1,1,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;172;-3874.966,1549.996;Inherit;False;Property;zhezhaoUliudong1;遮罩2U流动;31;0;Create;False;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;38;-2502.508,476.6294;Inherit;True;Property;UVniuquTex;UV扭曲;33;1;[Header];Create;False;1;05                                                            UVDistortTexture;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;MipLevel;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureCoordinatesNode;174;-3911.859,1381.643;Inherit;False;0;170;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureCoordinatesNode;90;-4881.397,612.3408;Inherit;False;1;-1;4;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;171;-3873.86,1668.878;Inherit;False;Property;zhezhaoVliudong1;遮罩2V流动;32;0;Create;False;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;428;-2175.478,569.6799;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;154;-4549.566,703.7683;Inherit;False;niuqu_Custom1Z;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;533;-3553.274,1491.612;Inherit;False;CalculateUV;-1;;51;878911039a84d384fbe16355229feed8;0;3;32;FLOAT2;0,0;False;16;FLOAT;0;False;17;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;548;-3145.711,1373.087;Inherit;False;Constant;_Float3;Float 3;70;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;170;-3304.31,1455.502;Inherit;True;Property;zhezhaoTex2;遮罩2;30;0;Create;False;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;482;-1971.169,872.7756;Inherit;False;Property;UVniuquqiangdu;UV扭曲强度;39;0;Create;False;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;155;-1992.927,749.2068;Inherit;False;154;niuqu_Custom1Z;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.ToggleSwitchNode;429;-2014.313,501.9643;Inherit;False;Property;kaiqiniuqufangxiang;开启扭曲反向;35;0;Create;False;0;0;0;False;0;False;0;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StaticSwitch;547;-2880.149,1407.604;Inherit;False;Property;_Keyword2;Keyword 2;25;0;Create;True;0;0;0;False;0;False;0;0;0;True;;Toggle;2;Key0;Key1;Reference;545;True;True;9;1;FLOAT;0;False;0;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT;0;False;7;FLOAT;0;False;8;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;487;-1732.161,784.915;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCRemapNode;245;-1744.095,506.1244;Inherit;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;-0.5;False;4;FLOAT;0.5;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;44;-1486.603,653.9718;Inherit;False;3;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;544;-1478.44,457.1284;Inherit;False;Constant;_Float1;Float 1;68;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;473;-5135.845,2075.823;Inherit;False;6695.528;758.153;;22;210;301;292;208;209;207;212;205;215;213;424;216;483;484;230;423;195;240;238;549;550;563;溶解;1,1,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;238;-5052.645,2186.795;Inherit;False;1053.486;614.5574;;7;232;198;197;196;494;522;534;UV流动;1,1,1,1;0;0
Node;AmplifyShaderEditor.StaticSwitch;543;-1279.44,623.1284;Inherit;False;Property;kaiqiniuqu;开启扭曲;34;0;Create;False;0;0;0;False;0;False;0;0;0;True;;Toggle;2;Key0;Key1;Create;True;True;9;1;FLOAT;0;False;0;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT;0;False;7;FLOAT;0;False;8;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;198;-4985.695,2513.633;Inherit;False;Property;rongjieVliudong;溶解V流动;55;0;Create;False;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;240;-3879.141,2246.127;Inherit;False;590.1461;382.9414;;4;475;476;204;298;扭曲;1,1,1,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;197;-4983.871,2401.769;Inherit;False;Property;rongjieUliudong;溶解U流动;54;0;Create;False;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.Vector4Node;232;-4993.126,2614.551;Inherit;False;Property;rongjiejizuobiaoTiling_Offset;溶解极坐标Tiling_Offset;48;0;Create;False;0;0;0;False;0;False;1,1,0,0;1,1,0,0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureCoordinatesNode;196;-5017.393,2241.94;Inherit;False;0;195;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RegisterLocalVarNode;296;-989.5841,625.0588;Inherit;False;UVniuqu;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;134;-4438.513,-682.608;Inherit;False;3644.035;868.467;;4;5;454;132;458;主纹理;1,1,1,1;0;0
Node;AmplifyShaderEditor.GetLocalVarNode;298;-3807.448,2448.177;Inherit;False;296;UVniuqu;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;475;-3865.84,2527.094;Inherit;False;Property;rongjieniuqu;溶解扭曲;43;0;Create;False;0;0;0;False;0;False;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;534;-4618.425,2331.603;Inherit;False;CalculateUV;-1;;55;878911039a84d384fbe16355229feed8;0;3;32;FLOAT2;0,0;False;16;FLOAT;0;False;17;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.FunctionNode;522;-4642.182,2514.114;Inherit;False;CalculatePolarCoordinatesUV;-1;;56;59e6323c63d3ead4f95451b440a41729;0;4;32;FLOAT2;0,0;False;16;FLOAT;0;False;17;FLOAT;0;False;23;FLOAT4;1,1,0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.CommentaryNode;458;-4373.079,-581.4863;Inherit;False;1399.036;724.0708;;11;91;97;96;438;79;16;19;20;491;535;523;UV流动;1,1,1,1;0;0
Node;AmplifyShaderEditor.StaticSwitch;494;-4242.018,2425.348;Inherit;False;Property;jizuobiao;极坐标;11;0;Create;False;0;0;0;False;0;False;0;0;0;True;;Toggle;2;Key0;Key1;Reference;491;True;True;9;1;FLOAT2;0,0;False;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT2;0,0;False;6;FLOAT2;0,0;False;7;FLOAT2;0,0;False;8;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;476;-3574.308,2449.553;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;204;-3439.032,2321.237;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.Vector4Node;79;-4324.738,-192.1013;Inherit;False;Property;zhuwenlijizuobiaoTiling_Offset;主纹理极坐标Tiling_Offset;12;0;Create;False;0;0;0;False;0;False;1,1,0,0;1,1,0,0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.CommentaryNode;335;-2904.031,-2546.111;Inherit;False;2658.673;677.6412;;14;328;326;327;324;323;325;321;322;319;320;318;317;315;316;菲涅尔;1,1,1,1;0;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;16;-4308.66,-519.3759;Inherit;False;0;5;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;96;-3982.202,-68.94512;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;488;-1985.749,2993.952;Inherit;False;3620.853;1223.012;;18;555;556;247;486;248;277;495;270;249;267;278;268;281;266;246;295;293;294;顶点偏移;1,1,1,1;0;0
Node;AmplifyShaderEditor.ViewDirInputsCoordNode;316;-2793.552,-2170.666;Inherit;False;World;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.WorldNormalVector;315;-2809.703,-2371.893;Inherit;False;False;1;0;FLOAT3;0,0,1;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.DynamicAppendNode;94;-4518.263,538.7935;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleAddOpNode;97;-3980.766,36.82314;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;195;-3255.959,2281.779;Inherit;True;Property;rongjietietu;溶解贴图;45;1;[Header];Create;False;1;06                                                            DissolveTexture;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;MipLevel;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.DesaturateOpNode;563;-2938.555,2210.612;Inherit;False;2;0;FLOAT3;0,0,0;False;1;FLOAT;1;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;19;-4280.446,-386.0668;Inherit;False;Property;zhuwenliUliudong;主纹理U流动;15;0;Create;False;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;91;-3984.437,-520.6965;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.CommentaryNode;294;-1275.713,3191.133;Inherit;False;615.5561;360.849;;4;300;258;489;490;扭曲;1,1,1,1;0;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;229;-4565.711,785.7122;Inherit;False;rongjiejindu_Custom1W;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DotProductOpNode;317;-2541.353,-2281.402;Inherit;False;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;167;-2199.744,1282.486;Inherit;False;2959.489;648.7829;;15;524;537;312;311;35;33;26;496;178;30;169;28;32;545;546;遮罩;1,1,1,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;20;-4280.102,-279.053;Inherit;False;Property;zhuwenliVliudong;主纹理V流动;16;0;Create;False;0;0;0;False;0;False;0;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;293;-1926.099,3117.271;Inherit;False;592.5952;438.4681;;4;252;257;251;536;UV流动;1,1,1,1;0;0
Node;AmplifyShaderEditor.DynamicAppendNode;438;-3779.955,-185.2457;Inherit;False;FLOAT4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.OneMinusNode;423;-2926.082,2348.871;Inherit;False;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;252;-1874.504,3442.734;Inherit;False;Property;dindianpianyiVliudong;顶点偏移V流动;62;0;Create;False;0;0;0;False;0;False;0;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;230;-2962.23,2509.771;Inherit;False;229;rongjiejindu_Custom1W;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;28;-2161.962,1353.486;Inherit;False;0;26;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;490;-1262.429,3468.272;Inherit;False;Property;dingdianpianyiniuqu;顶点偏移扭曲;44;0;Create;False;0;0;0;False;0;False;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.Vector4Node;178;-2151.07,1731.59;Inherit;False;Property;zhezhaojizuobiaoTiling_Offset1;遮罩极坐标Tiling_Offset;26;0;Create;False;0;0;0;False;0;False;1,1,0,0;1,1,0,0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.FunctionNode;535;-3534.41,-428.2841;Inherit;False;CalculateUV;-1;;61;878911039a84d384fbe16355229feed8;0;3;32;FLOAT2;0,0;False;16;FLOAT;0;False;17;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;257;-1882.994,3205.627;Inherit;False;0;246;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;251;-1874.25,3347.405;Inherit;False;Property;dindianpianyiUliudong;顶点偏移U流动;61;0;Create;False;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;169;-1181.539,1389.246;Inherit;False;585.3124;393.8479;UV扭曲;4;467;61;466;299;;1,1,1,1;0;0
Node;AmplifyShaderEditor.FunctionNode;523;-3575.924,-257.1703;Inherit;False;CalculatePolarCoordinatesUV;-1;;62;59e6323c63d3ead4f95451b440a41729;0;4;32;FLOAT2;0,0;False;16;FLOAT;0;False;17;FLOAT;0;False;23;FLOAT4;1,1,0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;32;-2130.057,1623.17;Inherit;False;Property;zhezhaoVliudong;遮罩V流动;28;0;Create;False;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.AbsOpNode;318;-2378.057,-2281.073;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;30;-2132.272,1523.738;Inherit;False;Property;zhezhaoUliudong;遮罩U流动;27;0;Create;False;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;484;-2978.958,2605.228;Inherit;False;Property;rongjiejindu;溶解进度;49;0;Create;False;0;0;0;False;0;False;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;300;-1209.281,3372.647;Inherit;False;296;UVniuqu;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;216;-2666.242,2656.246;Inherit;False;Constant;_Float4;Float 4;35;0;Create;True;0;0;0;False;0;False;-2;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;467;-1171.186,1697.779;Inherit;False;Property;zhezhaoniuqu;遮罩扭曲;42;0;Create;False;0;0;0;False;0;False;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;299;-1124.514,1601.747;Inherit;False;296;UVniuqu;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;537;-1750.185,1411.885;Inherit;False;CalculateUV;-1;;66;878911039a84d384fbe16355229feed8;0;3;32;FLOAT2;0,0;False;16;FLOAT;0;False;17;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleAddOpNode;483;-2674.958,2534.228;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ToggleSwitchNode;424;-2746.56,2278.75;Inherit;False;Property;kaiqirongjiefanx;开启溶解反向;47;0;Create;False;0;0;0;False;0;False;0;True;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.FunctionNode;536;-1560.376,3306.63;Inherit;False;CalculateUV;-1;;67;878911039a84d384fbe16355229feed8;0;3;32;FLOAT2;0,0;False;16;FLOAT;0;False;17;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.CommentaryNode;132;-2856.408,-536.7737;Inherit;False;914.8101;514.41;;7;85;89;88;87;86;84;83;Clamp;1,1,1,1;0;0
Node;AmplifyShaderEditor.StaticSwitch;491;-3231.179,-361.2939;Inherit;False;Property;jizuobiao;极坐标;11;0;Create;False;0;0;0;False;0;False;0;0;0;True;;Toggle;2;Key0;Key1;Create;True;True;9;1;FLOAT2;0,0;False;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT2;0,0;False;6;FLOAT2;0,0;False;7;FLOAT2;0,0;False;8;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.FunctionNode;524;-1785.859,1643.202;Inherit;False;CalculatePolarCoordinatesUV;-1;;68;59e6323c63d3ead4f95451b440a41729;0;4;32;FLOAT2;0,0;False;16;FLOAT;0;False;17;FLOAT;0;False;23;FLOAT4;1,1,0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;489;-971.4283,3368.272;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;319;-2209.04,-2360.011;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;258;-844.7635,3256.84;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.StaticSwitch;496;-1455.028,1558.92;Inherit;False;Property;_Keyword0;Keyword 0;11;0;Create;True;0;0;0;False;0;False;0;0;0;True;;Toggle;2;Key0;Key1;Reference;491;True;True;9;1;FLOAT2;0,0;False;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT2;0,0;False;6;FLOAT2;0,0;False;7;FLOAT2;0,0;False;8;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;215;-2450.186,2582.005;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;322;-1968.03,-2141.374;Inherit;False;Property;feinierruihua;菲尼尔锐化;70;0;Create;False;0;0;0;False;0;False;1;0.1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;213;-2451.532,2423.189;Inherit;False;Constant;_Float0;Float 0;35;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SwizzleNode;84;-2811.264,-227.3198;Inherit;False;FLOAT;1;0;2;3;1;0;FLOAT2;0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ComponentMaskNode;205;-2483.22,2276.478;Inherit;False;True;False;False;False;1;0;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ToggleSwitchNode;320;-2018.345,-2307.602;Inherit;False;Property;fanxiangfeinier;反向菲尼尔;68;0;Create;False;0;0;0;False;0;False;0;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SwizzleNode;83;-2818.264,-428.3197;Inherit;False;FLOAT;0;0;2;3;1;0;FLOAT2;0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;466;-873.962,1617.592;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;85;-2609.802,-151.0888;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.PowerNode;321;-1772.542,-2244.997;Inherit;False;False;2;0;FLOAT;0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;86;-2615.744,-346.4398;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;207;-2218.769,2536.697;Half;False;Property;rongjieruanyingdu;溶解软硬度;50;0;Create;False;0;0;0;False;0;False;0;0;0;0.5;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;61;-707.0469,1437.849;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.CommentaryNode;454;-1829.253,-428.697;Inherit;False;551.5576;381.5275;;4;461;297;462;46;扭曲;1,1,1,1;0;0
Node;AmplifyShaderEditor.SamplerNode;246;-556.6409,3458.855;Inherit;True;Property;dindianpianyi;顶点偏移;56;1;[Header];Create;False;1;07                                                            VertexOffsetTexture;0;0;False;0;False;-1;None;af1360d1f00334745a2b7059fb8a1f17;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.CommentaryNode;295;-1269.156,3633.281;Inherit;False;588.5105;545.6133;;4;271;273;272;538;顶点偏移遮罩;1,1,1,1;0;0
Node;AmplifyShaderEditor.SimpleAddOpNode;212;-2164.67,2323.865;Inherit;False;3;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;325;-1566.521,-2244.844;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;297;-1749.193,-254.4287;Inherit;False;296;UVniuqu;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;272;-1210.857,3924.581;Inherit;False;Property;dindianpianyizhezhaoUliudong;顶点偏移遮罩U流动;64;0;Create;False;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ToggleSwitchNode;87;-2439.479,-430.438;Inherit;False;Property;zhuwenliUClamp;主纹理U_Clamp;13;0;Create;False;0;0;0;False;0;False;0;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;281;-234.2016,3317.324;Inherit;False;Property;dindianneiwaiqiangdu;顶点内外强度;60;0;Create;False;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;461;-1798.134,-150.5936;Inherit;False;Property;zhuwenliniuqu;主纹理扭曲;40;0;Create;False;0;0;0;False;0;False;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;268;-236.7628,3637.779;Inherit;False;Property;dindianpianyigaodu;顶点外偏移高度;59;0;Create;False;0;0;0;False;0;False;0;0.7;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;209;-1973.797,2317.241;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;324;-1576.522,-2138.813;Inherit;False;Property;feinierqiangdu;菲尼尔强度;71;0;Create;False;0;0;0;False;0;False;1;1.22;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;208;-1911.736,2591.574;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ToggleSwitchNode;88;-2442.662,-227.8942;Inherit;False;Property;zhuwenliVClamp1;主纹理V_Clamp;14;0;Create;False;0;0;0;False;0;False;0;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCRemapNode;266;-237.0727,3450.151;Inherit;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;-0.5;False;4;FLOAT;0.5;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;26;-542.8676,1409.184;Inherit;True;Property;zhezhaoTex;遮罩;24;1;[Header];Create;False;1;04                                                            MaskTexture;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;MipLevel;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureCoordinatesNode;271;-1219.157,3758.787;Inherit;False;0;270;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;273;-1211.162,4044.875;Inherit;False;Property;dindianpianyizhezhaoVliudong;顶点偏移遮罩V流动;65;0;Create;False;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;538;-898.2962,3868.699;Inherit;False;CalculateUV;-1;;70;878911039a84d384fbe16355229feed8;0;3;32;FLOAT2;0,0;False;16;FLOAT;0;False;17;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.ColorNode;327;-1243.447,-2135.247;Inherit;False;Property;feinieryanse;菲尼尔颜色;66;2;[HDR];[Header];Create;False;1;08                                                            Fresnel;0;0;False;0;False;1,1,1,1;0.2220888,1.110444,1.844303,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;550;-1706.389,2213.246;Inherit;False;Constant;_Float5;Float 5;70;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;278;11.14474,3380.129;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;479;-83.53478,4456.553;Inherit;False;283.299;306.8566;;1;361;自定义数据2;1,1,1,1;0;0
Node;AmplifyShaderEditor.SmoothstepOpNode;210;-1729.532,2360.607;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;323;-1381.327,-2242.507;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;35;-351.9114,1659.67;Inherit;False;Property;zhezhaoqiangdu;遮罩强度;29;0;Create;False;0;0;0;False;0;False;0;0;-1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;267;17.84834,3526.216;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;462;-1501.022,-218.3074;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;33;-198.8515,1456.536;Inherit;False;3;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;367;-410.166,737.5797;Inherit;False;Property;DepthFade;软粒子;6;0;Create;False;0;0;0;False;0;False;0;0;0;10;0;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;89;-2136.007,-342.2273;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;361;-30.53135,4543.491;Inherit;False;2;-1;4;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;46;-1412.005,-377.2227;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleAddOpNode;495;267.988,3288.61;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DepthFade;364;-97.43309,726.877;Inherit;False;True;True;True;2;1;FLOAT3;0,0,0;False;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;326;-983.9163,-2242.947;Inherit;True;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StaticSwitch;549;-1358.415,2265.593;Inherit;False;Property;kaiqirongjie;开启溶解;46;0;Create;False;0;0;0;False;0;False;0;0;0;True;;Toggle;2;Key0;Key1;Create;True;True;9;1;FLOAT;0;False;0;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT;0;False;7;FLOAT;0;False;8;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;249;353.0395,3750.392;Inherit;False;Property;dindianpianyiqiangdu;顶点偏移强度;58;0;Create;False;0;0;0;False;0;False;0;0.03;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;270;-293.035,3729.82;Inherit;True;Property;dindianpianyizhezhao;顶点偏移遮罩;63;0;Create;False;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;311;0.809629,1453.613;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;472;-200.7079,-1245.609;Inherit;False;536.5843;338.5228;;2;137;14;主纹理颜色;1,1,1,1;0;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;301;-1084.083,2266.107;Inherit;False;rongjie;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;546;191.9232,1343.569;Inherit;False;Constant;_Float2;Float 2;69;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.NormalVertexDataNode;248;465.0632,3583.966;Inherit;False;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.CommentaryNode;471;-180.0646,-1645.288;Inherit;False;488.8154;310.4648;;2;141;140;顶点色;1,1,1,1;0;0
Node;AmplifyShaderEditor.SimpleAddOpNode;486;586.8929,3822.295;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;360;191.5186,245.1294;Inherit;False;Constant;_Float8;Float 8;66;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;312;180.7336,1451.006;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;558;172.2569,602.1785;Inherit;False;Constant;_Float7;Float 7;72;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ComponentMaskNode;329;-286.6484,314.7152;Inherit;False;False;False;False;True;1;0;COLOR;0,0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;277;509.5656,3280.363;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;365;177.6958,727.9739;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;409;-3788.543,-1672.15;Inherit;False;2815.474;816.6891;;8;541;542;376;368;375;457;410;560;附加贴图;1,1,1,1;0;0
Node;AmplifyShaderEditor.SamplerNode;5;-1162.05,-409.4184;Inherit;True;Property;zhuwenli;主纹理;9;0;Create;False;0;0;0;False;0;False;-1;None;14566cc117166fd48b027b91de9f41aa;True;0;False;white;Auto;False;Object;-1;MipLevel;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;12;341.8913,62.7554;Inherit;False;Property;zhengtitoumingdu;整体透明度;7;0;Create;False;0;0;0;False;0;False;1;1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.ToggleSwitchNode;422;481.8087,-182.523;Inherit;False;Property;shiyongRtongdao;使用R通道;10;0;Create;False;0;0;0;False;0;False;0;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;14;-154.579,-1154.902;Inherit;False;Property;zhuwenli_color;主纹理_颜色;8;2;[HDR];[Header];Create;False;1;02                                                            MainTexture;0;0;False;0;False;1,1,1,1;0.3693854,0.6712002,0.8603976,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StaticSwitch;358;571.1552,287.382;Inherit;False;Property;feiniermoshi1;菲尼尔模式;67;0;Create;False;0;0;0;False;0;False;0;0;0;True;;KeywordEnum;2;Add;Multiply;Reference;359;True;True;9;1;FLOAT;0;False;0;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT;0;False;7;FLOAT;0;False;8;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;410;-2718.343,-1375.894;Inherit;False;600.9932;347.3058;;4;381;382;463;464;扭曲;1,1,1,1;0;0
Node;AmplifyShaderEditor.VertexColorNode;140;-138.0103,-1563.399;Inherit;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.CommentaryNode;292;-731.95,2170.532;Inherit;False;2264.213;631.2297;;12;220;309;217;226;221;222;304;219;218;224;553;561;溶解亮边;1,1,1,1;0;0
Node;AmplifyShaderEditor.StaticSwitch;557;393.6442,641.8755;Inherit;False;Property;kaiqiruanlizi;开启软粒子;5;0;Create;False;0;0;0;False;0;False;0;0;0;True;;Toggle;2;Key0;Key1;Create;True;True;9;1;FLOAT;0;False;0;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT;0;False;7;FLOAT;0;False;8;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.Vector3Node;556;1029.442,3260.123;Inherit;False;Constant;_Vector0;Vector 0;71;0;Create;True;0;0;0;False;0;False;0,0,0;0,0,0;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;247;990.68,3453.668;Inherit;True;3;3;0;FLOAT;0;False;1;FLOAT3;0,0,0;False;2;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.CommentaryNode;457;-3755.167,-1540.791;Inherit;False;948.9048;656.5604;;7;396;371;370;374;493;525;539;UV流动;1,1,1,1;0;0
Node;AmplifyShaderEditor.StaticSwitch;545;376.6927,1416.342;Inherit;False;Property;kaiqizhezhao;开启遮罩;25;0;Create;False;0;0;0;False;0;False;0;0;0;True;;Toggle;2;Key0;Key1;Create;True;True;9;1;FLOAT;0;False;0;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT;0;False;7;FLOAT;0;False;8;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;302;527.6134,193.8027;Inherit;False;301;rongjie;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.StaticSwitch;493;-3085.863,-1254.864;Inherit;False;Property;_jizuobiao;jizuobiao;11;0;Create;True;0;0;0;False;0;False;0;0;0;True;;Toggle;2;Key0;Key1;Reference;491;True;True;9;1;FLOAT2;0,0;False;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT2;0,0;False;6;FLOAT2;0,0;False;7;FLOAT2;0,0;False;8;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleAddOpNode;559;349.5626,-425.4881;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;499;2808.497,472.552;Inherit;False;Property;Dst;Dst;1;1;[Enum];Create;False;0;0;1;UnityEngine.Rendering.BlendMode;True;0;False;10;10;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;463;-2705.464,-1115.528;Inherit;False;Property;fujiatietuniuqu;附加贴图扭曲;41;0;Create;False;0;0;0;False;0;False;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;309;190.7353,2514.235;Inherit;False;307;zhuwenliA;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.ComponentMaskNode;328;-530.3571,-2229.652;Inherit;False;True;True;True;False;1;0;COLOR;0,0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;304;1230.424,2520.679;Inherit;False;rongjiekuangduyanse;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;7;973.3407,-1553.752;Inherit;True;3;3;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.GetLocalVarNode;381;-2656.713,-1197.338;Inherit;False;296;UVniuqu;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;525;-3430.77,-1170.721;Inherit;False;CalculatePolarCoordinatesUV;-1;;71;59e6323c63d3ead4f95451b440a41729;0;4;32;FLOAT2;0,0;False;16;FLOAT;0;False;17;FLOAT;0;False;23;FLOAT4;1,1,0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleAddOpNode;346;1316.535,-1623.564;Inherit;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.StaticSwitch;385;-117.0303,-455.5948;Inherit;False;Property;fujiatietudiejiamoshi;附加贴图叠加模式;20;0;Create;False;0;0;0;False;0;False;0;0;0;True;;KeywordEnum;2;Add;Multiply;Create;True;True;9;1;COLOR;0,0,0,0;False;0;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;3;COLOR;0,0,0,0;False;4;COLOR;0,0,0,0;False;5;COLOR;0,0,0,0;False;6;COLOR;0,0,0,0;False;7;COLOR;0,0,0,0;False;8;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;375;-1901.952,-1176.008;Inherit;False;Property;fujiatietuyanse;附加贴图颜色;17;2;[HDR];[Header];Create;False;1;03                                                            AddTexture;0;0;False;0;False;0,0,0,1;0,0,0,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ComponentMaskNode;137;105.4577,-1155.988;Inherit;False;True;True;True;False;1;0;COLOR;0,0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;501;2806.32,275.1362;Inherit;False;Property;shenduceshi;深度测试;4;1;[Enum];Create;False;0;0;1;UnityEngine.Rendering.CompareFunction;True;0;False;4;4;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.StaticSwitch;350;1533.679,-1365.95;Inherit;False;Property;feiniermoshi;菲尼尔模式;69;0;Create;False;0;0;0;False;0;False;0;0;0;True;;KeywordEnum;2;Add;Multiply;Create;True;True;9;1;FLOAT3;0,0,0;False;0;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT3;0,0,0;False;5;FLOAT3;0,0,0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SamplerNode;368;-1962.981,-1408.929;Inherit;True;Property;fujiatietu;附加贴图;18;0;Create;False;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;MipLevel;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;561;718.3593,2647.184;Inherit;False;Constant;_Float6;Float 6;72;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;384;-301.5602,-374.3311;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;382;-2275.418,-1305.831;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;307;-655.6927,-141.23;Inherit;False;zhuwenliA;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;498;2796.663,65.50704;Inherit;False;Property;Cullmode;剔除模式;2;1;[Enum];Create;False;0;1;Option1;0;1;UnityEngine.Rendering.CullMode;True;0;False;0;2;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;370;-3692.405,-1319.469;Inherit;False;Property;fujiatietuUliudong;附加贴图U流动;22;0;Create;False;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.StaticSwitch;541;-1263.296,-1434.953;Inherit;False;Property;kaiqifujiatietu;开启附加贴图;19;0;Create;False;0;0;0;False;0;False;0;0;0;True;;Toggle;2;Key0;Key1;Create;True;True;9;1;COLOR;0,0,0,0;False;0;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;3;COLOR;0,0,0,0;False;4;COLOR;0,0,0,0;False;5;COLOR;0,0,0,0;False;6;COLOR;0,0,0,0;False;7;COLOR;0,0,0,0;False;8;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;371;-3692.885,-1216.448;Inherit;False;Property;fujiatietuVliudong;附加贴图V流动;23;0;Create;False;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;500;2807.997,385.5523;Inherit;False;Property;Src;Src;0;2;[Header];[Enum];Create;False;1;01                                                            BasicSettings;0;1;UnityEngine.Rendering.BlendMode;True;0;False;5;5;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;348;974.8106,-1269.247;Inherit;True;4;4;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.ComponentMaskNode;138;579.9492,-395.6387;Inherit;False;True;True;True;False;1;0;COLOR;0,0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;218;-503.6981,2358.532;Half;False;Property;rongjiemiaobianfanwei;溶解描边范围;51;0;Create;False;0;0;0;False;0;False;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;352;983.4429,-996.2236;Inherit;True;3;3;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;376;-1517.242,-1309.945;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;560;-1313.609,-1551.706;Inherit;False;Constant;_Float9;Float 9;72;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;374;-3715.332,-1487.021;Inherit;False;0;368;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StaticSwitch;359;1852.792,-1120.814;Inherit;False;Property;kaiqifeinier;开启菲尼尔;67;0;Create;False;0;0;0;False;0;False;0;0;0;True;;Toggle;2;Off;On;Create;True;True;9;1;FLOAT3;0,0,0;False;0;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT3;0,0,0;False;5;FLOAT3;0,0,0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.ColorNode;542;-1567.652,-1574.429;Inherit;False;Constant;_Color0;Color 0;67;0;Create;True;0;0;0;False;0;False;0,0,0,1;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;10;1536.301,-7.998409;Inherit;True;8;8;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT;0;False;7;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;226;313.396,2586.808;Inherit;False;Property;rongjieliangbianyanse;溶解亮边颜色;53;1;[HDR];Create;False;0;0;0;False;0;False;1,1,1,1;1,1,1,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector4Node;396;-3718.484,-1077.415;Inherit;False;Property;fujiatietujizuobiaoTiling_Offset1;附加贴图极坐标Tiling_Offset;21;0;Create;False;0;0;0;False;0;False;1,1,0,0;1,1,0,0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ComponentMaskNode;141;104.7219,-1572.894;Inherit;False;True;True;True;False;1;0;COLOR;0,0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleAddOpNode;379;-296.1505,-498.2511;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;222;164.093,2281.361;Inherit;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;464;-2430.822,-1180.68;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;219;-313.2518,2529.068;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;539;-3367.452,-1348.875;Inherit;False;CalculateUV;-1;;73;878911039a84d384fbe16355229feed8;0;3;32;FLOAT2;0,0;False;16;FLOAT;0;False;17;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.StepOpNode;221;-92.90669,2225.361;Inherit;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StaticSwitch;555;1285.206,3348.712;Inherit;False;Property;kaiqidingdianpianyi;开启顶点偏移;57;0;Create;False;0;0;0;False;0;False;0;0;0;True;;Toggle;2;Key0;Key1;Create;True;True;9;1;FLOAT3;0,0,0;False;0;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT3;0,0,0;False;5;FLOAT3;0,0,0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;224;699.7599,2436.35;Inherit;False;3;3;0;FLOAT;0;False;1;FLOAT;0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;306;-259.5564,-128.7505;Inherit;False;304;rongjiekuangduyanse;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;217;-648.949,2547.898;Half;False;Property;rongjiemiaobiankuangdu;溶解描边宽度;52;0;Create;False;0;0;0;False;0;False;0.5;0.5;0;0.5;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;497;2799.52,167.0365;Inherit;False;Property;shendu;深度;3;1;[Enum];Create;False;0;2;Off;0;On;1;0;True;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.StepOpNode;220;-98.90681,2506.361;Inherit;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StaticSwitch;553;958.0043,2514.691;Inherit;False;Property;kaiqirongjie2;开启溶解;46;0;Create;False;0;0;0;False;0;False;0;0;0;True;;Toggle;2;Key0;Key1;Reference;549;True;True;9;1;COLOR;0,0,0,0;False;0;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;3;COLOR;0,0,0,0;False;4;COLOR;0,0,0,0;False;5;COLOR;0,0,0,0;False;6;COLOR;0,0,0,0;False;7;COLOR;0,0,0,0;False;8;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;4;0,0;Float;False;False;-1;2;UnityEditor.ShaderGraph.PBRMasterGUI;0;3;New Amplify Shader;2992e84f91cbeb14eab234972e07ea9d;True;Meta;0;4;Meta;0;False;False;False;False;False;False;False;False;False;False;False;False;True;0;False;-1;False;True;0;False;-1;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;3;RenderPipeline=UniversalPipeline;RenderType=Opaque=RenderType;Queue=Geometry=Queue=0;True;0;0;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;2;False;-1;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;1;LightMode=Meta;False;0;Hidden/InternalErrorShader;0;0;Standard;0;False;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;2;0,0;Float;False;False;-1;2;UnityEditor.ShaderGraph.PBRMasterGUI;0;3;New Amplify Shader;2992e84f91cbeb14eab234972e07ea9d;True;ShadowCaster;0;2;ShadowCaster;0;False;False;False;False;False;False;False;False;False;False;False;False;True;0;False;-1;False;True;0;False;-1;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;3;RenderPipeline=UniversalPipeline;RenderType=Opaque=RenderType;Queue=Geometry=Queue=0;True;0;0;False;False;False;False;False;False;False;False;False;False;False;False;True;0;False;-1;False;False;False;False;False;False;False;False;False;False;False;False;False;True;1;False;-1;True;3;False;-1;False;True;1;LightMode=ShadowCaster;False;0;Hidden/InternalErrorShader;0;0;Standard;0;False;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;1;2740.975,711.5902;Float;False;True;-1;2;UnityEditor.ShaderGraph.PBRMasterGUI;0;3;Hoxi/Vfx;2992e84f91cbeb14eab234972e07ea9d;True;Forward;0;1;Forward;8;False;False;False;False;False;False;False;False;False;False;False;False;True;0;False;-1;True;True;2;True;498;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;3;RenderPipeline=UniversalPipeline;RenderType=Transparent=RenderType;Queue=Transparent=Queue=0;True;0;0;True;True;8;5;True;500;1;True;499;0;0;False;-1;0;False;-1;False;False;False;False;False;False;False;False;False;False;False;False;False;True;True;True;True;True;True;0;False;-1;False;False;False;False;False;False;True;True;False;255;False;-1;255;False;-1;255;False;-1;0;False;-1;1;False;-1;1;False;-1;1;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;True;True;1;True;497;True;3;True;501;True;True;0;False;-1;0;False;-1;True;1;LightMode=UniversalForward;False;0;Hidden/InternalErrorShader;0;0;Standard;22;Surface;1;  Blend;0;Two Sided;0;Cast Shadows;0;  Use Shadow Threshold;0;Receive Shadows;0;GPU Instancing;1;LOD CrossFade;0;Built-in Fog;0;DOTS Instancing;0;Meta Pass;0;Extra Pre Pass;0;Tessellation;0;  Phong;0;  Strength;0.5,False,-1;  Type;0;  Tess;16,False,-1;  Min;10,False,-1;  Max;25,False,-1;  Edge Length;16,False,-1;  Max Displacement;25,False,-1;Vertex Position,InvertActionOnDeselection;1;0;5;False;True;False;True;False;False;;False;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;3;0,0;Float;False;False;-1;2;UnityEditor.ShaderGraph.PBRMasterGUI;0;3;New Amplify Shader;2992e84f91cbeb14eab234972e07ea9d;True;DepthOnly;0;3;DepthOnly;0;False;False;False;False;False;False;False;False;False;False;False;False;True;0;False;-1;False;True;0;False;-1;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;3;RenderPipeline=UniversalPipeline;RenderType=Opaque=RenderType;Queue=Geometry=Queue=0;True;0;0;False;False;False;False;False;False;False;False;False;False;False;False;True;0;False;-1;False;False;False;True;False;False;False;False;0;False;-1;False;False;False;False;False;False;False;False;False;True;1;False;-1;False;False;True;1;LightMode=DepthOnly;False;0;Hidden/InternalErrorShader;0;0;Standard;0;False;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;0;698.4423,-56.326;Float;False;False;-1;2;UnityEditor.ShaderGraph.PBRMasterGUI;0;3;New Amplify Shader;2992e84f91cbeb14eab234972e07ea9d;True;ExtraPrePass;0;0;ExtraPrePass;5;False;False;False;False;False;False;False;False;False;False;False;False;True;0;False;-1;False;True;0;False;-1;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;3;RenderPipeline=UniversalPipeline;RenderType=Opaque=RenderType;Queue=Geometry=Queue=0;True;0;0;False;True;1;1;False;-1;0;False;-1;0;1;False;-1;0;False;-1;False;False;False;False;False;False;False;False;False;False;False;False;True;0;False;-1;False;True;True;True;True;True;0;False;-1;False;False;False;False;False;False;False;True;False;255;False;-1;255;False;-1;255;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;False;True;1;False;-1;True;3;False;-1;True;True;0;False;-1;0;False;-1;True;0;False;0;Hidden/InternalErrorShader;0;0;Standard;0;False;0
WireConnection;526;32;47;0
WireConnection;526;16;48;0
WireConnection;526;17;50;0
WireConnection;526;23;146;0
WireConnection;540;32;47;0
WireConnection;540;16;48;0
WireConnection;540;17;50;0
WireConnection;492;1;540;0
WireConnection;492;0;526;0
WireConnection;38;1;492;0
WireConnection;428;0;38;1
WireConnection;154;0;90;3
WireConnection;533;32;174;0
WireConnection;533;16;172;0
WireConnection;533;17;171;0
WireConnection;170;1;533;0
WireConnection;429;0;38;1
WireConnection;429;1;428;0
WireConnection;547;1;548;0
WireConnection;547;0;170;1
WireConnection;487;0;155;0
WireConnection;487;1;482;0
WireConnection;245;0;429;0
WireConnection;44;0;245;0
WireConnection;44;1;487;0
WireConnection;44;2;547;0
WireConnection;543;1;544;0
WireConnection;543;0;44;0
WireConnection;296;0;543;0
WireConnection;534;32;196;0
WireConnection;534;16;197;0
WireConnection;534;17;198;0
WireConnection;522;32;196;0
WireConnection;522;16;197;0
WireConnection;522;17;198;0
WireConnection;522;23;232;0
WireConnection;494;1;534;0
WireConnection;494;0;522;0
WireConnection;476;0;298;0
WireConnection;476;1;475;0
WireConnection;204;0;494;0
WireConnection;204;1;476;0
WireConnection;96;0;90;1
WireConnection;96;1;79;3
WireConnection;94;0;90;1
WireConnection;94;1;90;2
WireConnection;97;0;90;2
WireConnection;97;1;79;4
WireConnection;195;1;204;0
WireConnection;563;0;195;0
WireConnection;91;0;16;0
WireConnection;91;1;94;0
WireConnection;229;0;90;4
WireConnection;317;0;315;0
WireConnection;317;1;316;0
WireConnection;438;0;79;1
WireConnection;438;1;79;2
WireConnection;438;2;96;0
WireConnection;438;3;97;0
WireConnection;423;0;563;0
WireConnection;535;32;91;0
WireConnection;535;16;19;0
WireConnection;535;17;20;0
WireConnection;523;32;16;0
WireConnection;523;16;19;0
WireConnection;523;17;20;0
WireConnection;523;23;438;0
WireConnection;318;0;317;0
WireConnection;537;32;28;0
WireConnection;537;16;30;0
WireConnection;537;17;32;0
WireConnection;483;0;230;0
WireConnection;483;1;484;0
WireConnection;424;0;563;0
WireConnection;424;1;423;0
WireConnection;536;32;257;0
WireConnection;536;16;251;0
WireConnection;536;17;252;0
WireConnection;491;1;535;0
WireConnection;491;0;523;0
WireConnection;524;32;28;0
WireConnection;524;16;30;0
WireConnection;524;17;32;0
WireConnection;524;23;178;0
WireConnection;489;0;300;0
WireConnection;489;1;490;0
WireConnection;319;0;318;0
WireConnection;258;0;536;0
WireConnection;258;1;489;0
WireConnection;496;1;537;0
WireConnection;496;0;524;0
WireConnection;215;0;483;0
WireConnection;215;1;216;0
WireConnection;84;0;491;0
WireConnection;205;0;424;0
WireConnection;320;0;319;0
WireConnection;320;1;318;0
WireConnection;83;0;491;0
WireConnection;466;0;299;0
WireConnection;466;1;467;0
WireConnection;85;0;84;0
WireConnection;321;0;320;0
WireConnection;321;1;322;0
WireConnection;86;0;83;0
WireConnection;61;0;496;0
WireConnection;61;1;466;0
WireConnection;246;1;258;0
WireConnection;212;0;205;0
WireConnection;212;1;213;0
WireConnection;212;2;215;0
WireConnection;325;0;321;0
WireConnection;87;0;83;0
WireConnection;87;1;86;0
WireConnection;209;0;212;0
WireConnection;208;0;207;0
WireConnection;88;0;84;0
WireConnection;88;1;85;0
WireConnection;266;0;246;1
WireConnection;26;1;61;0
WireConnection;538;32;271;0
WireConnection;538;16;272;0
WireConnection;538;17;273;0
WireConnection;278;0;281;0
WireConnection;278;1;266;0
WireConnection;210;0;209;0
WireConnection;210;1;207;0
WireConnection;210;2;208;0
WireConnection;323;0;325;0
WireConnection;323;1;324;0
WireConnection;267;0;266;0
WireConnection;267;1;268;0
WireConnection;462;0;297;0
WireConnection;462;1;461;0
WireConnection;33;0;26;1
WireConnection;33;1;26;4
WireConnection;33;2;170;1
WireConnection;89;0;87;0
WireConnection;89;1;88;0
WireConnection;46;0;89;0
WireConnection;46;1;462;0
WireConnection;495;0;278;0
WireConnection;495;1;267;0
WireConnection;364;0;367;0
WireConnection;326;0;323;0
WireConnection;326;1;327;0
WireConnection;549;1;550;0
WireConnection;549;0;210;0
WireConnection;270;1;538;0
WireConnection;311;0;33;0
WireConnection;311;1;35;0
WireConnection;301;0;549;0
WireConnection;486;0;249;0
WireConnection;486;1;361;1
WireConnection;312;0;311;0
WireConnection;329;0;326;0
WireConnection;277;0;495;0
WireConnection;277;1;270;1
WireConnection;365;0;364;0
WireConnection;5;1;46;0
WireConnection;422;0;5;4
WireConnection;422;1;5;1
WireConnection;358;1;360;0
WireConnection;358;0;329;0
WireConnection;557;1;558;0
WireConnection;557;0;365;0
WireConnection;247;0;277;0
WireConnection;247;1;248;0
WireConnection;247;2;486;0
WireConnection;545;1;546;0
WireConnection;545;0;312;0
WireConnection;493;1;539;0
WireConnection;493;0;525;0
WireConnection;559;0;385;0
WireConnection;559;1;306;0
WireConnection;328;0;326;0
WireConnection;304;0;553;0
WireConnection;7;0;137;0
WireConnection;7;1;138;0
WireConnection;7;2;141;0
WireConnection;525;32;374;0
WireConnection;525;16;370;0
WireConnection;525;17;371;0
WireConnection;525;23;396;0
WireConnection;346;0;328;0
WireConnection;346;1;7;0
WireConnection;385;1;379;0
WireConnection;385;0;384;0
WireConnection;137;0;14;0
WireConnection;350;1;346;0
WireConnection;350;0;348;0
WireConnection;368;1;382;0
WireConnection;384;0;541;0
WireConnection;384;1;5;0
WireConnection;382;0;493;0
WireConnection;382;1;464;0
WireConnection;307;0;5;4
WireConnection;541;1;560;0
WireConnection;541;0;376;0
WireConnection;348;0;141;0
WireConnection;348;1;137;0
WireConnection;348;2;138;0
WireConnection;348;3;328;0
WireConnection;138;0;559;0
WireConnection;352;0;141;0
WireConnection;352;1;137;0
WireConnection;352;2;138;0
WireConnection;376;0;368;0
WireConnection;376;1;375;0
WireConnection;359;1;352;0
WireConnection;359;0;350;0
WireConnection;10;0;422;0
WireConnection;10;1;14;4
WireConnection;10;2;12;0
WireConnection;10;3;545;0
WireConnection;10;4;302;0
WireConnection;10;5;358;0
WireConnection;10;6;140;4
WireConnection;10;7;557;0
WireConnection;141;0;140;0
WireConnection;379;0;5;0
WireConnection;379;1;541;0
WireConnection;222;0;220;0
WireConnection;222;1;221;0
WireConnection;464;0;381;0
WireConnection;464;1;463;0
WireConnection;219;0;210;0
WireConnection;219;1;217;0
WireConnection;539;32;374;0
WireConnection;539;16;370;0
WireConnection;539;17;371;0
WireConnection;221;0;218;0
WireConnection;221;1;210;0
WireConnection;555;1;556;0
WireConnection;555;0;247;0
WireConnection;224;0;222;0
WireConnection;224;1;309;0
WireConnection;224;2;226;0
WireConnection;220;0;218;0
WireConnection;220;1;219;0
WireConnection;553;1;561;0
WireConnection;553;0;224;0
WireConnection;1;2;359;0
WireConnection;1;3;10;0
WireConnection;1;5;555;0
ASEEND*/
//CHKSM=1ADFEEC09BD3DD2E337DD2BA313C3BBC05F870FA