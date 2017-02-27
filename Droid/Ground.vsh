﻿attribute vec3 Vertices;
attribute vec3 Normals;
attribute vec2 TextureCoordinates;
uniform mat3 Rotation;
uniform mat3 Model;
uniform mat4 ModelViewProjection;
uniform mat4 ViewProjection;
varying vec3 Surface;
varying vec3 Normal;
varying vec2 TextureCoordinate;

void main()
{
	Model;
	Rotation;
	Surface = Vertices;
	Normal = Normals;
	TextureCoordinate = TextureCoordinates;
	gl_Position = ModelViewProjection * vec4(Vertices, 1.0);
}