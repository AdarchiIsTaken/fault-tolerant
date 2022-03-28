(async () => {
    Vue.createApp({
        data() {
            return {
                todoDescription: "",
                todos: []
            }
        },
        async mounted() {
            await this.loadTodos();

            console.table(this.todos);
        },
        methods: {
            async addTodo() {
                if (this.todoDescription.trim() === "") {
                    return;
                }

                const response = await fetch('/todo', {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json'
                    },
                    body: JSON.stringify({ description: this.todoDescription.trim() })
                }).then(r => r.json());
    
                this.todos.push(response.data);
            },
            async loadTodos() {
                const response = await fetch('/todo').then(r => r.json());
                this.todos = response.data
            },
            async deleteTodo(id) {
                await fetch(`/todo/${id}`, { method: 'DELETE' });
                this.todos = this.todos.filter(t => t.id !== id);
            }
        }
    }).mount('#app')
})();

