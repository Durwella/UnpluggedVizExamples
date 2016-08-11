attribute vec3 Vertices;
uniform mat4 ModelViewProjection;
uniform mat3 Rotation;

void main()
{
	Rotation;
    gl_Position = ModelViewProjection * vec4(Vertices, 1.0);
}
