uniform vec3 LightPosition;
varying vec3 Surface;
//varying vec3 Normal;
varying vec2 TextureCoordinate;
uniform sampler2D TextureMap;
uniform sampler2D ColorMap;
uniform mat3 Rotation;
//uniform mat4 ViewProjection;
varying float IsNull;
//uniform sampler2D NullMap;

vec4 ColorFromColorMap(float value, sampler2D colorMap, float colorMapOffset);

void main()
{
    float value = texture2D(TextureMap, TextureCoordinate).r;

    if (IsNull < 0.9999)
        discard;

    float delta = 0.01;
    float dx = 
        -(texture2D(TextureMap, TextureCoordinate + vec2(delta, 0.0)).r - texture2D(TextureMap, TextureCoordinate).r) / delta;
    float dz = 
        -(texture2D(TextureMap, TextureCoordinate + vec2(0.0, delta)).r - texture2D(TextureMap, TextureCoordinate).r) / delta;
    vec3 N = Rotation * normalize(vec3(dx, 1.0, dz));

    float ambient = 0.1;
    vec3 L = normalize(LightPosition - Surface);
    float shading = ambient + abs(dot(N,L));
    float colorMapOffset = 0.0;
    vec4 color = ColorFromColorMap(value, ColorMap, colorMapOffset);
    gl_FragColor = vec4(shading * color.rgb, color.a);
}
