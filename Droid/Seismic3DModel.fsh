uniform vec3 LightPosition;
varying vec3 Surface;
varying vec3 Normal;
varying vec2 TextureCoordinate;
uniform sampler2D TextureMap;
uniform sampler2D ColorMap;

vec4 ColorFromColorMap(float value, sampler2D colorMap, float colorMapOffset);

void main()
{
    vec3 L = normalize(LightPosition - Surface);
    vec3 N = normalize(Normal);
    float ambient = 0.1;
    float shading = ambient + abs(dot(N,L)); // abs for 2-sided
    float value = texture2D(TextureMap, TextureCoordinate).r;
    float colorMapOffset = 0.0;
    vec4 color = ColorFromColorMap(value, ColorMap, colorMapOffset);
	gl_FragColor =  vec4(shading * color.rgb, color.a);
}
