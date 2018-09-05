<?php
class ArrayValue implements JsonSerializable {
    public function __construct(array $array) {
        $this->array = $array;
    }

    public function jsonSerialize() {
        return $this->array;
    }
}

	function isValidJSON($str) {
	   json_decode($str);
	   return json_last_error() == JSON_ERROR_NONE;
	}

	$json_params = file_get_contents("php://input");

	if (strlen($json_params) > 0 && isValidJSON($json_params))
	  $decoded_params = json_decode($json_params,true);

	$consulta = $decoded_params['tipoConsulta'];
	$clave = $decoded_params['Clave'];

	if($consulta == 'Revista')
	{
		$array = array('tipoConsulta' => $consulta,'Clave' => $clave,'Version' => '1');
	}

	if($consulta == 'UltimaEdicion')
	{
		$array = array('tipoConsulta' => $consulta,'Clave' => $clave,'Version' => '1');	
	}
	
	if($consulta == 'Edicion')
	{
		$array = array('tipoConsulta' => $consulta,'Clave' => $clave,'Version' => '1');	
	}
	
	if($consulta == 'Ediciones')
	{
		$array = array('tipoConsulta' => $consulta,'Clave' => $clave,'Version' => '1');	
	}

	if($consulta == 'Articulo')
	{
		$array = array('tipoConsulta' => $consulta,'Clave' => $clave,'Version' => '1');	
	}
	
	if($consulta == 'Regional')
	{
		$array = array('tipoConsulta' => $consulta,'Clave' => $clave,'Version' => '1');	
	}
	
	if($consulta == 'Regionales')
	{
		$array = array('tipoConsulta' => $consulta,'Clave' => $clave,'Version' => '1');	
	}
	
	if($consulta == 'Destacados')
	{
		$array = array('tipoConsulta' => $consulta,'Clave' => $clave,'Version' => '1');	
	}
	
	if($consulta == 'Favoritos')
	{
		$array = array('tipoConsulta' => $consulta,'Clave' => $clave,'Version' => '1');	
	}
	
	if($consulta == 'Editorial')
	{
		$array = array('tipoConsulta' => $consulta,'Clave' => $clave,'Version' => '1');	
	}
	
	if($consulta == 'AcercaDe')
	{
		$array = array('tipoConsulta' => $consulta,'Clave' => $clave,'Version' => '1');	
	}
	
	if($consulta == 'Politicas')
	{
		$array = array('tipoConsulta' => $consulta,'Clave' => $clave,'Version' => '1');	
	}
	


	echo json_encode(new ArrayValue($array), JSON_PRETTY_PRINT);
?>