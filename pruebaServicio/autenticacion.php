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

$correo = $decoded_params['Email'];
$clave = $decoded_params['Password'];

if($correo == 'abc@icbf.gov.co' && $clave=='abcABC2*')
{
	$array = ['valido' => 'true', 'mensaje' => 'Se autenticó con exito'];
}
else
{
	$array = ['valido' => 'false', 'mensaje' => 'Lo sentimos. El usuario se encuentra DESACTIVADO. Por favor comuniquese con la mesa de ayuda al correo mis@icbf.gov.co con el asunto DESACTIVADO, y especificando su actor del ICBF'];	
}

echo json_encode(new ArrayValue($array), JSON_PRETTY_PRINT);
?>