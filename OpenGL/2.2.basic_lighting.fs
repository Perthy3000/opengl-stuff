#version 330 core
out vec4 FragColor;

in vec3 Normal;  
in vec3 FragPos;  
  
uniform vec3 lightPos[3]; 
uniform vec3 viewPos; 
uniform vec3 lightColor[3];
uniform vec3 objectColor;

void main()
{
	vec3 result = vec3(0.0);

	for(int i=0; i<3; i++) {
		// ambient
		float ambientStrength = 0.1;
		vec3 ambient = ambientStrength * lightColor[i];
  	
		// diffuse 
		vec3 norm = normalize(Normal);
		vec3 lightDir = normalize(lightPos[i] - FragPos);
		float diff = max(dot(norm, lightDir), 0.0);
		vec3 diffuse = diff * lightColor[i];
    
		// specular
		float specularStrength = 0.5;
		vec3 viewDir = normalize(viewPos - FragPos);
		vec3 reflectDir = reflect(-lightDir, norm);  
		float spec = pow(max(dot(viewDir, reflectDir), 0.0), 32);
		vec3 specular = specularStrength * spec * lightColor[i];  
        
		result += (ambient + diffuse + specular) * objectColor;
	}

    FragColor = vec4(result, 1.0);
}
