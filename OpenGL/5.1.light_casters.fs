#version 330 core
out vec4 FragColor;

struct Material {
    sampler2D diffuse;
    sampler2D specular;    
    float shininess;
}; 

struct Light {
    //vec3 position;
    vec3 direction;

    vec3 ambient;
    vec3 diffuse;
    vec3 specular;
};

in vec3 FragPos;  
in vec3 Normal;  
in vec2 TexCoords;
  
uniform vec3 viewPos;
uniform Material material;
uniform Light light;
uniform sampler2D texture_diffuse1;
uniform vec3 objectColor;
uniform vec3 lightColor;
uniform vec3 lightPos;

void main()
{
	vec3 result = vec3(0.0);
    // ambient
    //vec3 ambient = light.ambient * texture(texture_diffuse1, TexCoords).rgb;
	//vec3 ambient = light.ambient * texture(material.diffuse, TexCoords).rgb;
  	
    // diffuse 
    //vec3 norm = normalize(Normal);
    // vec3 lightDir = normalize(light.position - FragPos);
    vec3 lightDir = normalize(-light.direction);  
    //float diff = max(dot(norm, lightDir), 0.0);
	//vec3 diffuse = light.diffuse * diff * texture(texture_diffuse1, TexCoords).rgb;
    //vec3 diffuse = light.diffuse * diff * texture(material.diffuse, TexCoords).rgb;
    
    // specular
    //vec3 viewDir = normalize(viewPos - FragPos);
    //vec3 reflectDir = reflect(-lightDir, norm);  
    //float spec = pow(max(dot(viewDir, reflectDir), 0.0), material.shininess);
    //vec3 specular = light.specular * spec * texture(texture_diffuse1, TexCoords).rgb;
    //vec3 specular = light.specular * spec * texture(material.specular, TexCoords).rgb;

	// ambient
	float ambientStrength = 0.1;
	vec3 ambient = ambientStrength * lightColor;
  	
	// diffuse 
	vec3 norm = normalize(Normal);
	//vec3 lightDir = normalize(lightPos - FragPos);
	float diff = max(dot(norm, lightDir), 0.0);
	vec3 diffuse = diff * lightColor;
    
	// specular
	float specularStrength = 0.5;
	vec3 viewDir = normalize(viewPos - FragPos);
	vec3 reflectDir = reflect(-lightDir, norm);  
	float spec = pow(max(dot(viewDir, reflectDir), 0.0), 32);
	vec3 specular = specularStrength * spec * lightColor;  
        
	result += (ambient + diffuse + specular) * objectColor;
        
    //vec3 result = ambient + diffuse + specular;
    FragColor = vec4(result, 1.0);
	// FragColor = vec4(0.9, 0.2, 0.2, 1.0);
} 