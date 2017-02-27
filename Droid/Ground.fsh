uniform vec3 LightPosition;
varying vec3 Surface;
varying vec3 Normal;
varying vec2 TextureCoordinate;
uniform mat4 ModelViewProjection;
uniform sampler2D Image;

void main()
{
    vec3 L = normalize(LightPosition - Surface);
    vec3 rotatedNormal = vec3(ModelViewProjection * vec4(Normal, 1.0));
    vec3 N = normalize(Normal);
    float minimumShading = 0.0;
    float shading = max(dot(N,L), minimumShading);
    vec4 color = texture2D(Image, TextureCoordinate);
    gl_FragColor = shading * color;
}