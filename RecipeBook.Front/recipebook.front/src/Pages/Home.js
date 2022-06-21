import { useEffect, useState } from "react";
import { Button, Spinner } from "react-bootstrap";
import { Container, Row, Card } from "react-bootstrap";
import { Link } from "react-router-dom";

function Home() {
    const [recipes, setRecipes] = useState();
    const [dataIsLoading, setDataIsLoading] = useState(true);

    const getData = () => {
        fetch(`https://localhost:7073/api/recipes`)
            .then((res) => res.json())
            .then((res) => {
                setRecipes(res);
                setDataIsLoading(false);
            })
    }

    useEffect(() => {
        getData();
    }, [])

    return (
        <>
            {dataIsLoading ?
                <Button variant="dark" disabled style={{ margin: "auto", display: "flex", paddingTop: "2%" }}>
                    <Spinner
                        as="span"
                        animation="border"
                        size="xs"
                        role="status"
                        aria-hidden="true"
                    />
                    <h2>Loading</h2>
                </Button>
                :
                <div>
                    <div>
                        <h1>
                            Все рецепты
                        </h1>
                    </div>
                    <div>
                        <Container>
                            <Row xs={12} sm={8} md={4}>
                                {recipes.map(recipe => 
                                     <Card key={recipe.id} style={{marginLeft:"83px", marginTop:"15px", marginBottom:"10px"}}>
                                     <Link to={`/recipes/${recipe.id}`} className="productCartBody">
                                         <Card.Img variant="top" src={`http://localhost:3000/${recipe.image}`} />
                                         <Card.Body>
                                             <Card.Title>{recipe.recipeName}</Card.Title>
                                          </Card.Body>
                                     </Link>
                                     </Card>
                                )}
                            </Row>
                        </Container>
                    </div>
                </div>
            }
        </>
    )
} export default Home