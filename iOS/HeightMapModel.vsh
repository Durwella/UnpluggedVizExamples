attribute vec3 Vertices;
//attribute vec3 Normals;
attribute vec2 TextureCoordinates;
uniform mat3 Rotation;
uniform mat4 ModelViewProjection;
varying vec3 Surface;
//varying vec3 Normal;
varying vec2 TextureCoordinate;
varying float IsNull;
uniform sampler2D TextureMap;
uniform sampler2D NullMap;
uniform float ColorMapMinimum;

void main()
{
    //Normal = Rotation * Normals;
    TextureCoordinate = TextureCoordinates;
    float height = texture2D(TextureMap, TextureCoordinate).r;
    height = max(ColorMapMinimum, height);
    IsNull = height >= (60.0 / 256.0) ? 1.0 : 0.0; // Treat height of 0 as null value
    //IsNull = texture2D(NullMap, TextureCoordinate).r;
    Surface = vec3(Vertices.x, Vertices.y + height, Vertices.z);
    gl_Position = ModelViewProjection * vec4(Surface, 1.0);
}
