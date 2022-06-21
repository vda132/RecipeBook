import React, { useEffect, useState } from "react";
import { useParams } from "react-router-dom";

function RecipePage() {
    const [recipe, setRecipe] = useState();
    const [dataIsLoading, setDataIsLoading] = useState(true);
    const { id } = useParams();

    const getData = () => {
        fetch(`https://localhost:7073/api/recipes/${id}`)
            .then((res) => res.json())
            .then((res) => {
                console.log(res)
                setRecipe(res);
                setDataIsLoading(false)
            });
    }

    useEffect(() => {
        getData();
    }, [])

    return (
        <>
            {dataIsLoading ? <h1>Loading...</h1>
                :
                <div >
                    <div style={{ textAlign:"center" }}>
                        <h2>{recipe.recipeName}</h2>
                    </div>
                    <div>
                        <img src={`http://localhost:3000/${recipe.image}`} style={{ margin: "auto" }}></img>
                    </div>
                    <div>
                        <h3>
                            Рецепт
                        </h3>
                        <h4>
                            {recipe.description}
                        </h4>
                    </div>
                </div>
            }

        </>
    )
} export default RecipePage