attribute vec3 Vertices;
uniform mat3 Rotation;
uniform mat3 Model;
uniform mat4 ModelViewProjection;
uniform mat4 ViewProjection;

void main()
{
	Model;
	ViewProjection;
    Rotation;
    ModelViewProjection;
    gl_Position = vec4(Vertices, 1.0);
}