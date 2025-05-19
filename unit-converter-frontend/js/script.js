document.addEventListener('DOMContentLoaded', function() {
    // API base URL - updated to use port 5001
    const API_BASE_URL = 'http://localhost:5001';
    
    // DOM elements
    const conversionTypeSelect = document.getElementById('conversion-type');
    const fromUnitSelect = document.getElementById('from-unit');
    const toUnitSelect = document.getElementById('to-unit');
    const inputValue = document.getElementById('input-value');
    const resultValue = document.getElementById('result-value');
    const convertBtn = document.getElementById('convert-btn');
    const swapUnitsBtn = document.getElementById('swap-units');
    const conversionHistory = document.getElementById('conversion-history');
    
    // Unit options for each conversion type
    const unitOptions = {
        length: ['Meter', 'Kilometer', 'Centimeter', 'Millimeter', 'Mile', 'Yard', 'Foot', 'Inch'],
        weight: ['Kilogram', 'Gram', 'Milligram', 'Pound', 'Ounce', 'Ton'],
        temperature: ['Celsius', 'Fahrenheit', 'Kelvin']
    };
    
    // Initialize the unit selects based on the selected conversion type
    function updateUnitSelects() {
        const selectedType = conversionTypeSelect.value;
        const units = unitOptions[selectedType];
        
        // Clear existing options
        fromUnitSelect.innerHTML = '';
        toUnitSelect.innerHTML = '';
        
        // Add new options
        units.forEach(unit => {
            fromUnitSelect.add(new Option(unit, unit));
            toUnitSelect.add(new Option(unit, unit));
        });
        
        // Set default "to" unit to something different than "from" unit
        if (units.length > 1) {
            toUnitSelect.selectedIndex = 1;
        }
    }
    
    // Perform the conversion
    async function performConversion() {
        const value = parseFloat(inputValue.value);
        if (isNaN(value)) {
            alert('Please enter a valid number');
            return;
        }
        
        const fromUnit = fromUnitSelect.value;
        const toUnit = toUnitSelect.value;
        const conversionType = conversionTypeSelect.value;
        
        // Capitalize first letter of conversion type for the API endpoint
        const capitalizedType = conversionType.charAt(0).toUpperCase() + conversionType.slice(1);
        
        // Create the request payload exactly as specified
        const payload = {
            "value": value,
            "fromUnit": fromUnit,
            "toUnit": toUnit
        };
        
        // Log the request details for debugging
        console.log(`Making POST request to: ${API_BASE_URL}/convert/${capitalizedType}`);
        console.log('Request payload:', payload);
        
        try {
            const response = await fetch(`${API_BASE_URL}/convert/${capitalizedType}`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                    'Accept': '*/*'
                },
                body: JSON.stringify(payload)
            });
            
            console.log('Response status:', response.status);
            
            if (!response.ok) {
                const errorText = await response.text();
                console.error('Error response:', errorText);
                throw new Error(`API error: ${response.status} - ${errorText}`);
            }
            
            const data = await response.json();
            console.log('Response data:', data);
            
            // Update the result field based on the API response
            // Adjust this based on your actual API response structure
            if (typeof data === 'number') {
                resultValue.value = data.toFixed(6);
                addToHistory(value, fromUnit, data, toUnit, conversionType);
            } else if (data.result !== undefined) {
                resultValue.value = data.result.toFixed(6);
                addToHistory(value, fromUnit, data.result, toUnit, conversionType);
            } else {
                resultValue.value = "Invalid response";
                console.error("Unexpected API response format:", data);
            }
            
        } catch (error) {
            console.error('Error during conversion:', error);
            alert('Error during conversion. Please check the console for details.');
        }
    }
    
    // Add conversion to history
    function addToHistory(fromValue, fromUnit, toValue, toUnit, type) {
        const historyItem = document.createElement('div');
        historyItem.className = 'history-item';
        historyItem.innerHTML = `
            <strong>${type.charAt(0).toUpperCase() + type.slice(1)}:</strong> 
            ${fromValue} ${fromUnit} = ${typeof toValue === 'number' ? toValue.toFixed(6) : toValue} ${toUnit}
        `;
        
        conversionHistory.prepend(historyItem);
        
        // Limit history items
        if (conversionHistory.children.length > 10) {
            conversionHistory.removeChild(conversionHistory.lastChild);
        }
    }
    
    // Swap units
    function swapUnits() {
        const tempIndex = fromUnitSelect.selectedIndex;
        fromUnitSelect.selectedIndex = toUnitSelect.selectedIndex;
        toUnitSelect.selectedIndex = tempIndex;
        
        // If there's a result, swap the values too
        if (resultValue.value) {
            const tempValue = inputValue.value;
            inputValue.value = resultValue.value;
            resultValue.value = tempValue;
        }
    }
    
    // Event listeners
    conversionTypeSelect.addEventListener('change', updateUnitSelects);
    convertBtn.addEventListener('click', performConversion);
    swapUnitsBtn.addEventListener('click', swapUnits);
    
    // Initialize unit selects on page load
    updateUnitSelects();
});