import { useEffect, useState } from 'react';
import './App.css';

function App() {
    const [movies, setMovies] = useState();
    const [newTitle, setNewTitle] = useState("");

    useEffect(() => {
        getMovies();
    }, []);

    const baseUrl = "/api/movies";

    const contents = movies === undefined
        ? <p><em>Loading... Please refresh once the ASP.NET backend has started.</em></p>
        : <div>

            <input
                type="text"
                placeholder="Movie title"
                value={newTitle}
                onChange={e => setNewTitle(e.target.value)}
            />

            <button
                onClick={() => {
                    if (newTitle.trim() !== "") {
                        createMovie(newTitle);
                        setNewTitle("");
                    }
                }}
            >
                Add Movie
            </button>

            <table>
                <thead>
                    <tr>
                        <th>Title</th>
                        <th>Watched</th>
                        <th>Rating</th>
                        <th></th>
                    </tr>
                </thead>
                <tbody>
                    {movies.map(movie =>
                        <tr key={movie.id}>
                            <td>{movie.title}</td>

                            <td>
                                <input
                                    type="checkbox"
                                    checked={movie.isWatched}
                                    onChange={e =>
                                        updateMovie({
                                            ...movie,
                                            isWatched: e.target.checked
                                        })
                                    }
                                />
                            </td>

                            <td>
                                <select
                                    value={movie.rating ?? ""}
                                    onChange={e => {
                                        const value = e.target.value === "" ? null : Number(e.target.value);
                                        updateMovie({ ...movie, rating: value });
                                    }}
                                >
                                    <option value="">(none)</option>
                                    {[...Array(10)].map((_, i) =>
                                        <option key={i + 1} value={i + 1}>{i + 1}</option>
                                    )}
                                </select>
                            </td>

                            <td>
                                <button
                                    className="delete-button"
                                    onClick={() => deleteMovie(movie.id)}
                                >
                                    Delete
                                </button>
                            </td>
                        </tr>
                    )}
                </tbody>
            </table>

        </div>;

    return (
        <div>
            <h1>Movies</h1>
            {contents}
        </div>
    );

    async function getMovies() {
        const response = await fetch(baseUrl);
        if (response.ok) {
            const data = await response.json();
            setMovies(data);
        }
    }

    async function createMovie(title) {
        const response = await fetch(baseUrl, {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({ title, isWatched: false, rating: null }),
        });

        if (response.ok) {
            const data = await response.json();
            setMovies(prev => [...prev, data]);
        }
    }

    async function updateMovie(movie) {
        const response = await fetch(`${baseUrl}/${movie.id}`, {
            method: "PUT",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(movie),
        });

        if (response.ok) {
            setMovies(prev =>
                prev.map(m => (m.id === movie.id ? movie : m))
            );
        }
    }

    async function deleteMovie(id) {
        const response = await fetch(`${baseUrl}/${id}`, {
            method: "DELETE",
        });

        if (response.ok) {
            setMovies(prev => prev.filter(m => m.id !== id));
        }
    }
}

export default App;